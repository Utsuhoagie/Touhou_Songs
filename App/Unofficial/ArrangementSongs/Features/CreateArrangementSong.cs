using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record CreateArrangementSongCommand : IRequest<CreateArrangementSongResponse>
{
	public string Title { get; set; }
	public string Url { get; set; }

	public required string CircleName { get; set; }

	public required List<int> OfficialSongIds { get; set; }

	public CreateArrangementSongCommand(string title, string url) => (Title, Url) = (title, url);
}

public record CreateArrangementSongResponse
{
	public string Title { get; set; }
	public string Url { get; set; }
	public string Status { get; set; }

	public required string CircleName { get; set; }

	public required List<string> OfficialSongTitles { get; set; }

	public CreateArrangementSongResponse(string title, string url, string status)
		=> (Title, Url, Status) = (title, url, status);
}

class CreateArrangementSongCommandHandler : BaseHandler<CreateArrangementSongCommand, CreateArrangementSongResponse>
{
	public CreateArrangementSongCommandHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

	public override async Task<CreateArrangementSongResponse> Handle(CreateArrangementSongCommand command, CancellationToken cancellationToken)
	{
		var userRole = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

		if (userRole is null)
		{
			throw new AppException(HttpStatusCode.Unauthorized);
		}

		var circle = await _context.Circles.SingleOrDefaultAsync(c => c.Name == command.CircleName);

		if (circle is null)
		{
			throw new AppException(HttpStatusCode.NotFound, $"Circle with name = {command.CircleName} not found");
		}

		var officialSongs = await _context.OfficialSongs
			.Include(os => os.ArrangementSongs)
			.Where(os => command.OfficialSongIds.Contains(os.Id))
			.ToListAsync();

		var arrangementSongStatus = userRole == AuthRoles.Admin ? UnofficialStatus.Confirmed : UnofficialStatus.Pending;

		var arrangementSong = new ArrangementSong(command.Title, command.Url, arrangementSongStatus)
		{
			CircleId = circle.Id,
			Circle = circle,
			OfficialSongs = officialSongs,
			OfficialSongArrangementSongs = new(),
		};

		_context.ArrangementSongs.Add(arrangementSong);
		await _context.SaveChangesAsync();

		return new CreateArrangementSongResponse(arrangementSong.Title, arrangementSong.Url, arrangementSong.Status.ToString())
		{
			CircleName = arrangementSong.Circle.Name,
			OfficialSongTitles = arrangementSong.OfficialSongs.Select(os => os.Title).ToList(),
		};
	}
}
