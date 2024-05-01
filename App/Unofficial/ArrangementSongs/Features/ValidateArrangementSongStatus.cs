using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record ValidateArrangementSongStatusCommand(int Id, string Status) : IRequest<Result<string>>;

class ValidateArrangementSongStatusHandler : BaseHandler<ValidateArrangementSongStatusCommand, string, Result<string>>
{
	public ValidateArrangementSongStatusHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(ValidateArrangementSongStatusCommand command, CancellationToken cancellationToken)
	{
		var arrangementSong = await _context.ArrangementSongs.SingleOrDefaultAsync(a => a.Id == command.Id);

		if (arrangementSong is null)
		{
			return NotFound($"ArrangementSong {command.Id} not found.");
		}

		if (arrangementSong.Status != UnofficialStatus.Pending)
		{
			return Conflict($"ArrangementSong {command.Id} is already approved. Status = {arrangementSong.Status}.");
		}

		arrangementSong.Status = Enum.Parse<UnofficialStatus>(command.Status);
		await _context.SaveChangesAsync();

		var message = $"ArrangementSong [{command.Id}] was {command.Status} successfully.";
		return Ok(message);
	}
}
