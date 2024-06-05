using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record ValidateArrangementSongStatusCommand(int Id, string Status) : IRequest<Result<string>>;

class ValidateArrangementSongStatusHandler : BaseHandler<ValidateArrangementSongStatusCommand, string>
{
	public ValidateArrangementSongStatusHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(ValidateArrangementSongStatusCommand command, CancellationToken cancellationToken)
	{
		var dbArrangementSong = await _context.ArrangementSongs.SingleOrDefaultAsync(a => a.Id == command.Id);

		if (dbArrangementSong is null)
		{
			return _resultFactory.NotFound($"ArrangementSong {command.Id} not found.");
		}

		if (dbArrangementSong.Status != UnofficialStatus.Pending)
		{
			return _resultFactory.Conflict($"ArrangementSong {command.Id} is already approved. Status = {dbArrangementSong.Status}.");
		}

		dbArrangementSong.Status = Enum.Parse<UnofficialStatus>(command.Status);
		await _context.SaveChangesAsync();

		var message = $"ArrangementSong [{command.Id}] was {command.Status} successfully.";
		return _resultFactory.Ok(message);
	}
}
