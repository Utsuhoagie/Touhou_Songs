using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialSongs.Features;

public record GetOfficialSongsQuery(string? SearchTitle, string? GameCode) : IRequest<Result<IEnumerable<OfficialSongResponse>>>;

public record OfficialSongResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string Context { get; set; }

	public required string GameCode { get; set; }

	public OfficialSongResponse(OfficialSong officialSong) : base(officialSong)
		=> (Title, Context) = (officialSong.Title, officialSong.Context);
}

class GetOfficialSongsHandler : BaseHandler<GetOfficialSongsQuery, IEnumerable<OfficialSongResponse>>
{
	public GetOfficialSongsHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<OfficialSongResponse>>> Handle(GetOfficialSongsQuery query, CancellationToken cancellationToken)
	{
		var officialSongs_Res = await _context.OfficialSongs
			.Include(os => os.Game)
			.Where(os =>
				(query.SearchTitle == null || EF.Functions.ILike(os.Title, $"%{query.SearchTitle}%"))
				&& (query.GameCode == null || os.Game.GameCode == query.GameCode))
			.OrderBy(os => os.GameId)
				.ThenBy(os => os.Id)
			.Select(os => new OfficialSongResponse(os)
			{
				GameCode = os.Game.GameCode
			})
			.ToListAsync();

		return _resultFactory.Ok(officialSongs_Res);
	}
}
