using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record GetArrangementSongDetailQuery(int Id) : IRequest<Result<ArrangementSongDetailResponse>>;

public record ArrangementSongDetailResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string Url { get; set; }
	public UnofficialStatus Status { get; set; }

	public required string CircleName { get; set; }
	public required List<OfficialSongSimpleResponse> OfficialSongs { get; set; }

	public ArrangementSongDetailResponse(ArrangementSong arrangementSong) : base(arrangementSong)
		=> (Title, Url, Status) = (arrangementSong.Title, arrangementSong.Url, arrangementSong.Status);
}

public record OfficialSongSimpleResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }

	public OfficialSongSimpleResponse(OfficialSong officialSong) : base(officialSong)
		=> (Title) = (officialSong.Title);
}

class GetArrangementSongDetailHandler : BaseHandler<GetArrangementSongDetailQuery, ArrangementSongDetailResponse>
{
	public GetArrangementSongDetailHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<ArrangementSongDetailResponse>> Handle(GetArrangementSongDetailQuery query, CancellationToken cancellationToken)
	{
		var arrangementSong_Res = await _context.ArrangementSongs
			.Include(a => a.Circle)
			.Include(a => a.OfficialSongs)
			.Where(a => a.Id == query.Id)
			.Select(a => new ArrangementSongDetailResponse(a)
			{
				CircleName = a.Circle.Name,
				OfficialSongs = a.OfficialSongs.Select(os => new OfficialSongSimpleResponse(os)).ToList(),
			})
			.SingleOrDefaultAsync();

		if (arrangementSong_Res is null)
		{
			return _resultFactory.NotFound($"Arrangement Song with Id = {query.Id} not found.");
		}

		return _resultFactory.Ok(arrangementSong_Res);
	}
}
