using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record GetArrangementSongDetailQuery(int Id) : IRequest<Result<ArrangementSongDetailResponse>>;

public record ArrangementSongDetailResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Url { get; set; }
	public string Status { get; set; }

	public required string CircleName { get; set; }
	public required IEnumerable<OfficialSongSimpleResponse> OfficialSongs { get; set; }

	public ArrangementSongDetailResponse(int id, string title, string url, string status)
		=> (Id, Title, Url, Status) = (id, title, url, status);
}

public record OfficialSongSimpleResponse
{
	public int Id { get; set; }
	public string Title { get; set; }

	public OfficialSongSimpleResponse(int id, string title) => (Id, Title) = (id, title);
}

class GetArrangementSongDetailHandler : BaseHandler<GetArrangementSongDetailQuery, ArrangementSongDetailResponse>
{
	public GetArrangementSongDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<ArrangementSongDetailResponse>> Handle(GetArrangementSongDetailQuery query, CancellationToken cancellationToken)
	{
		var arrangementSong_Res = await _context.ArrangementSongs
			.Include(a => a.Circle)
			.Include(a => a.OfficialSongs)
			.Where(a => a.Id == query.Id)
			.Select(a => new ArrangementSongDetailResponse(a.Id, a.Title, a.Url, a.Status.ToString())
			{
				CircleName = a.Circle.Name,
				OfficialSongs = a.OfficialSongs.Select(os => new OfficialSongSimpleResponse(os.Id, os.Title)),
			})
			.SingleOrDefaultAsync();

		if (arrangementSong_Res is null)
		{
			return _resultFactory.NotFound($"Arrangement Song with Id = {query.Id} not found.");
		}

		return _resultFactory.Ok(arrangementSong_Res);
	}
}
