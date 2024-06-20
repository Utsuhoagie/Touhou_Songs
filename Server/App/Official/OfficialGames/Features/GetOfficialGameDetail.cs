using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialGames.Features;

public record GetOfficialGameDetailQuery(string GameCode) : IRequest<Result<OfficialGameDetailResponse>>;

public record OfficialGameDetailResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string GameCode { get; set; }
	public string NumberCode { get; set; }
	public DateTime ReleaseDate { get; set; }
	public string ImageUrl { get; set; }

	public required IEnumerable<OfficialSongSimple> Songs { get; set; }
	public record OfficialSongSimple : BaseEntityResponse
	{
		public string Title { get; set; }
		public string Context { get; set; }

		public OfficialSongSimple(OfficialSong officialSong) : base(officialSong)
			=> (Title, Context) = (officialSong.Title, officialSong.Context);
	}

	public OfficialGameDetailResponse(OfficialGame officialGame) : base(officialGame)
		=> (Title, GameCode, NumberCode, ReleaseDate, ImageUrl)
		= (officialGame.Title, officialGame.GameCode, officialGame.NumberCode, officialGame.ReleaseDate, officialGame.ImageUrl);
}

class GetOfficialGameDetailHandler : BaseHandler<GetOfficialGameDetailQuery, OfficialGameDetailResponse>
{
	public GetOfficialGameDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<OfficialGameDetailResponse>> Handle(GetOfficialGameDetailQuery query, CancellationToken cancellationToken)
	{
		var officialGameDetail_Res = await _context.OfficialGames
			.Include(og => og.Songs)
			.Where(og => og.GameCode == query.GameCode)
			.Select(og => new OfficialGameDetailResponse(og)
			{
				Songs = og.Songs
					.OrderBy(os => os.Id)
					.Select(os => new OfficialGameDetailResponse.OfficialSongSimple(os)),
			})
			.SingleOrDefaultAsync();

		if (officialGameDetail_Res is null)
		{
			return _resultFactory.NotFound($"OfficialGame {query.GameCode} not found.");
		}

		return _resultFactory.Ok(officialGameDetail_Res);
	}
}
