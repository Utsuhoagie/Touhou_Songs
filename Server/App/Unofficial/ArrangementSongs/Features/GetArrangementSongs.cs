using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Paging;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record GetArrangementSongsQuery(string? SearchTitle) : PagingParams, IRequest<Result<Paged<ArrangementSongResponse>>>;

public record ArrangementSongResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string Url { get; set; }
	public UnofficialStatus Status { get; set; }

	public required string CircleName { get; set; }
	public required List<string> OfficialSongTitles { get; set; }

	public ArrangementSongResponse(ArrangementSong arrangementSong) : base(arrangementSong)
		=> (Title, Url, Status) = (arrangementSong.Title, arrangementSong.Url, arrangementSong.Status);
}

class GetArrangementSongsHandler : BaseHandler<GetArrangementSongsQuery, Paged<ArrangementSongResponse>>
{
	public GetArrangementSongsHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<Paged<ArrangementSongResponse>>> Handle(GetArrangementSongsQuery query, CancellationToken cancellationToken)
	{
		var getArrangementSongsQuery = _context.ArrangementSongs
			.Include(a => a.Circle)
			.Include(a => a.OfficialSongs)
			.Where(a => query.SearchTitle == null || EF.Functions.Like(a.Title, $"%{query.SearchTitle}%"))
			.OrderBy(a => a.Title);

		var arrangementSongs_Res = await getArrangementSongsQuery
			.Skip((query.Page - 1) * query.PageSize)
			.Take(query.PageSize)
			.Select(a => new ArrangementSongResponse(a)
			{
				CircleName = a.Circle.Name,
				OfficialSongTitles = a.OfficialSongs
					.Select(os => os.Game)
					.Select(og => og.Title)
					.ToList(),
			})
			.ToListAsync();

		var totalArrangementSongs = await getArrangementSongsQuery.CountAsync();

		var pagedArrangementSongs_Res = new Paged<ArrangementSongResponse>(query)
		{
			TotalItemsCount = totalArrangementSongs,
			Items = arrangementSongs_Res,
		};

		return _resultFactory.Ok(pagedArrangementSongs_Res);
	}
}
