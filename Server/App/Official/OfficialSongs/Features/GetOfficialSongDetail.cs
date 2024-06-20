using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialSongs.Features;

public record GetOfficialSongDetailQuery(int Id) : IRequest<Result<OfficialSongDetailResponse>>;

public record OfficialSongDetailResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string Context { get; set; }

	public required OfficialGameSimple Game { get; set; }
	public record OfficialGameSimple : BaseAuditedEntityResponse
	{
		public string Title { get; set; }
		public string GameCode { get; set; }
		public string ImageUrl { get; set; }

		public OfficialGameSimple(OfficialGame officialGame) : base(officialGame)
			=> (Title, GameCode, ImageUrl) = (officialGame.Title, officialGame.GameCode, officialGame.ImageUrl);
	}

	public OfficialSongDetailResponse(OfficialSong officialSong) : base(officialSong)
		=> (Title, Context) = (officialSong.Title, officialSong.Context);
}

class GetOfficialSongDetailHandler : BaseHandler<GetOfficialSongDetailQuery, OfficialSongDetailResponse>
{
	public GetOfficialSongDetailHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<OfficialSongDetailResponse>> Handle(GetOfficialSongDetailQuery query, CancellationToken cancellationToken)
	{
		var officialSongDetail_Res = await _context.OfficialSongs
			.Include(os => os.Game)
			.Where(os => os.Id == query.Id)
			.Select(os => new OfficialSongDetailResponse(os)
			{
				Game = new OfficialSongDetailResponse.OfficialGameSimple(os.Game),
			})
			.SingleOrDefaultAsync();

		if (officialSongDetail_Res is null)
		{
			return _resultFactory.NotFound($"OfficialSong {query.Id} not found.");
		}

		return _resultFactory.Ok(officialSongDetail_Res);
	}
}
