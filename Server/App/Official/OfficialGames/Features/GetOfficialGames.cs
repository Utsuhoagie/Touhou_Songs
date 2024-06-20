using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialGames.Features;

public record GetOfficialGamesQuery(string? searchTitle) : IRequest<Result<IEnumerable<OfficialGameResponse>>>;

public record OfficialGameResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string GameCode { get; set; }
	public string NumberCode { get; set; }
	public DateTime ReleaseDate { get; set; }
	public string ImageUrl { get; set; }

	public required IEnumerable<string> SongTitles { get; set; }

	public OfficialGameResponse(OfficialGame officialGame) : base(officialGame)
		=> (Title, GameCode, NumberCode, ReleaseDate, ImageUrl)
		= (officialGame.Title, officialGame.GameCode, officialGame.NumberCode, officialGame.ReleaseDate, officialGame.ImageUrl);
}

class GetOfficialGamesHandler : BaseHandler<GetOfficialGamesQuery, IEnumerable<OfficialGameResponse>>
{
	public GetOfficialGamesHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<OfficialGameResponse>>> Handle(GetOfficialGamesQuery query, CancellationToken cancellationToken)
	{
		var officialGames_Res = await _context.OfficialGames
			.Include(og => og.Songs)
			.Where(og => query.searchTitle == null || EF.Functions.ILike(og.Title, $"%{query.searchTitle}%"))
			.OrderBy(og => og.ReleaseDate)
			.Select(og => new OfficialGameResponse(og)
			{
				SongTitles = og.Songs
					.OrderBy(os => os.Id)
					.Select(os => os.Title),
			})
			.ToListAsync();

		return _resultFactory.Ok(officialGames_Res);
	}
}
