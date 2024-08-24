using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Paging;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialGames.Features;

public record GetOfficialGamesQuery(string? SearchTitle) : PagingParams, IRequest<Result<Paged<OfficialGameResponse>>>;

public record OfficialGameResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string GameCode { get; set; }
	public string NumberCode { get; set; }
	public DateTime ReleaseDate { get; set; }
	public string ImageUrl { get; set; }

	public required List<string> SongTitles { get; set; }

	public OfficialGameResponse(OfficialGame officialGame) : base(officialGame)
		=> (Title, GameCode, NumberCode, ReleaseDate, ImageUrl)
		= (officialGame.Title, officialGame.GameCode, officialGame.NumberCode, officialGame.ReleaseDate, officialGame.ImageUrl);
}

class GetOfficialGamesHandler : BaseHandler<GetOfficialGamesQuery, Paged<OfficialGameResponse>>
{
	public GetOfficialGamesHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<Paged<OfficialGameResponse>>> Handle(GetOfficialGamesQuery query, CancellationToken cancellationToken)
	{
		var getOfficialGamesQuery = _context.OfficialGames
			.Include(og => og.Songs)
			.Where(og => query.SearchTitle == null || EF.Functions.ILike(og.Title, $"%{query.SearchTitle}%"))
			.OrderBy(og => og.ReleaseDate);

		var officialGames_Res = await getOfficialGamesQuery
			.Skip((query.Page - 1) * query.PageSize)
			.Take(query.PageSize)
			.Select(og => new OfficialGameResponse(og)
			{
				SongTitles = og.Songs
					.OrderBy(os => os.Id)
					.Select(os => os.Title)
					.ToList(),
			})
			.ToListAsync();

		var totalOfficialGames = await getOfficialGamesQuery.CountAsync();

		var pagedOfficialGames_Res = new Paged<OfficialGameResponse>(query)
		{
			TotalItemsCount = totalOfficialGames,
			Items = officialGames_Res,
		};

		return _resultFactory.Ok(pagedOfficialGames_Res);
	}
}
