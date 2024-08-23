using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record ValidateArrangementSongStatusPayload(UnofficialStatus Status);

public record ValidateArrangementSongStatusCommand(int Id, ValidateArrangementSongStatusPayload Payload) : IRequest<Result<string>>;

class ValidateArrangementSongStatusHandler : BaseHandler<ValidateArrangementSongStatusCommand, string>
{
	public ValidateArrangementSongStatusHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(ValidateArrangementSongStatusCommand command, CancellationToken cancellationToken)
	{
		var dbArrangementSong = await _context.ArrangementSongs
			.SingleOrDefaultAsync(a => a.Id == command.Id);

		if (dbArrangementSong is null)
		{
			return _resultFactory.NotFound(GenericI18n.NotFound.ToLanguage(Lang.EN, nameof(ArrangementSong), command.Id));
		}

		if (dbArrangementSong.Status != UnofficialStatus.Pending)
		{
			return _resultFactory.Conflict(GenericI18n.Conflict.ToLanguage(Lang.EN, $"ArrangementSong {command.Id} is already approved. Status = {dbArrangementSong.Status}."));
		}

		dbArrangementSong.Status = command.Payload.Status;
		await _context.SaveChangesAsync();

		var message = $"ArrangementSong [{dbArrangementSong.Id}] was {dbArrangementSong.Status} successfully.";
		return _resultFactory.Ok(GenericI18n.Success.ToLanguage(Lang.EN, message));
	}
}
