using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Official.OfficialGames.Features;

public record GetOfficialGameDetailQuery(string GameCode) : IRequest<Result<OfficialGameDetailResponse>>;

public record OfficialGameDetailResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string GameCode { get; set; }
	public string NumberCode { get; set; }
	public DateTime ReleaseDate { get; set; }
	public string ImageUrl { get; set; }

	public required IEnumerable<OfficialSongSimple> Songs { get; set; }
	public record OfficialSongSimple
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Context { get; set; }

		public OfficialSongSimple(int id, string title, string context) => (Id, Title, Context) = (id, title, context);
	}

	public OfficialGameDetailResponse(int id, string title, string gameCode, string numberCode, DateTime releaseDate, string imageUrl)
		=> (Id, Title, GameCode, NumberCode, ReleaseDate, ImageUrl) = (id, title, gameCode, numberCode, releaseDate, imageUrl);
}

class GetOfficialGameDetailHandler : BaseHandler<GetOfficialGameDetailQuery, OfficialGameDetailResponse, Result<OfficialGameDetailResponse>>
{
	public GetOfficialGameDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<OfficialGameDetailResponse>> Handle(GetOfficialGameDetailQuery query, CancellationToken cancellationToken)
	{
		var officialGameDetail_Res = await _context.OfficialGames
			.Include(og => og.Songs)
			.Where(og => og.GameCode == query.GameCode)
			.Select(og => new OfficialGameDetailResponse(og.Id, og.Title, og.GameCode, og.NumberCode, og.ReleaseDate, og.ImageUrl)
			{
				Songs = og.Songs
					.OrderBy(os => os.Id)
					.Select(os => new OfficialGameDetailResponse.OfficialSongSimple(os.Id, os.Title, os.Context)),
			})
			.SingleOrDefaultAsync();

		if (officialGameDetail_Res is null)
		{
			return NotFound($"OfficialGame {query.GameCode} not found.");
		}

		return Ok(officialGameDetail_Res);
	}
}
