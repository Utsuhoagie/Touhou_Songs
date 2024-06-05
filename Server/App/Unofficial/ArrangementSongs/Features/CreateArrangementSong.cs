using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record CreateArrangementSongCommand : IRequest<Result<CreateArrangementSongResponse>>
{
	public string Title { get; set; }
	public string? TitleRomaji { get; set; }
	public string? TitleJapanese { get; set; }

	public string Url { get; set; }

	public required string CircleName { get; set; }

	public required List<int> OfficialSongIds { get; set; }

	public CreateArrangementSongCommand(string title, string? titleRomaji, string? titleJapanese, string url)
		=> (Title, TitleRomaji, TitleJapanese, Url) = (title, titleRomaji, titleJapanese, url);
}

public record CreateArrangementSongResponse
{
	public string Title { get; set; }
	public string? TitleRomaji { get; set; }
	public string? TitleJapanese { get; set; }

	public string Url { get; set; }
	public string Status { get; set; }

	public required string CircleName { get; set; }

	public required List<string> OfficialSongTitles { get; set; }

	public CreateArrangementSongResponse(string title, string? titleRomaji, string? titleJapanese, string url, string status)
		=> (Title, TitleRomaji, TitleJapanese, Url, Status) = (title, titleRomaji, titleJapanese, url, status);
}

class CreateArrangementSongHandler : BaseHandler<CreateArrangementSongCommand, CreateArrangementSongResponse>
{
	public CreateArrangementSongHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<CreateArrangementSongResponse>> Handle(CreateArrangementSongCommand command, CancellationToken cancellationToken)
	{
		var userWithRole_Res = await _authUtils.GetUserWithRole();

		// NOT REQUIRED, SINCE IT THROWS ALREADY
		if (!userWithRole_Res.Success)
		{
			return _resultFactory.FromResult(userWithRole_Res);
		}

		var (user, role) = userWithRole_Res.Value;

		var dbCircle = await _context.Circles.SingleOrDefaultAsync(c => c.Name == command.CircleName);

		if (dbCircle is null)
		{
			return _resultFactory.NotFound($"Circle with name = {command.CircleName} not found");
		}

		var dbOfficialSongs = await _context.OfficialSongs
			.Include(os => os.ArrangementSongs)
			.Where(os => command.OfficialSongIds.Contains(os.Id))
			.ToListAsync();

		var arrangementSongStatus = role == AuthRole.Admin ?
			UnofficialStatus.Confirmed
			: UnofficialStatus.Pending;

		var arrangementSong = new ArrangementSong(command.Title, command.Url, arrangementSongStatus)
		{
			CircleId = dbCircle.Id,
			Circle = dbCircle,
			OfficialSongs = dbOfficialSongs,
			OfficialSongArrangementSongs = new(),
		};

		_context.ArrangementSongs.Add(arrangementSong);
		await _context.SaveChangesAsync();

		var createArrangementSong_Res = new CreateArrangementSongResponse(
			arrangementSong.Title, arrangementSong.TitleRomaji, arrangementSong.TitleJapanese,
			arrangementSong.Url, Enum.GetName(arrangementSong.Status)!)
		{
			CircleName = arrangementSong.Circle.Name,
			OfficialSongTitles = arrangementSong.OfficialSongs.Select(os => os.Title).ToList(),
		};

		return _resultFactory.Ok(createArrangementSong_Res);
	}
}
