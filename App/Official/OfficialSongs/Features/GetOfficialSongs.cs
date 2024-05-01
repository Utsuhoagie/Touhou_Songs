using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Official.OfficialSongs.Features;

public record GetOfficialSongsQuery(string? SearchTitle, string? GameCode) : IRequest<Result<IEnumerable<OfficialSongResponse>>>;

public record OfficialSongResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Context { get; set; }

	public required string GameCode { get; set; }

	public OfficialSongResponse(int id, string title, string context)
		=> (Id, Title, Context) = (id, title, context);
}

class GetOfficialSongsHandler : BaseHandler<GetOfficialSongsQuery, IEnumerable<OfficialSongResponse>, Result<IEnumerable<OfficialSongResponse>>>
{
	public GetOfficialSongsHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<OfficialSongResponse>>> Handle(GetOfficialSongsQuery query, CancellationToken cancellationToken)
	{
		var officialSongs_Res = await _context.OfficialSongs
			.Include(os => os.Game)
			.Where(os => query.SearchTitle == null || EF.Functions.ILike(os.Title, $"%{query.SearchTitle}%"))
			.Where(os => query.GameCode == null || os.Game.GameCode == query.GameCode)
			.OrderBy(os => os.GameId).ThenBy(os => os.Id)
			.Select(os => new OfficialSongResponse(os.Id, os.Title, os.Context)
			{
				GameCode = os.Game.GameCode
			})
			.ToListAsync();

		return Ok(officialSongs_Res);
	}
}
