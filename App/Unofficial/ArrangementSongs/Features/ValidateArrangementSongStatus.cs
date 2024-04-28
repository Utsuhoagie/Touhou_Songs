﻿using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record ValidateArrangementSongStatusCommand(int Id, string Status) : IRequest<string>;

class ValidateArrangementSongStatusCommandHandler : BaseHandler<ValidateArrangementSongStatusCommand, string>
{
	public ValidateArrangementSongStatusCommandHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

	public override async Task<string> Handle(ValidateArrangementSongStatusCommand command, CancellationToken cancellationToken)
	{
		var arrangementSong = await _context.ArrangementSongs.SingleOrDefaultAsync(a => a.Id == command.Id);

		if (arrangementSong is null)
		{
			throw new AppException(HttpStatusCode.NotFound, $"ArrangementSong {command.Id} not found.");
		}

		if (arrangementSong.Status != UnofficialStatus.Pending)
		{
			throw new AppException(HttpStatusCode.Conflict, $"ArrangementSong {command.Id} is already approved. Status = {arrangementSong.Status.ToString()}.");
		}

		arrangementSong.Status = Enum.Parse<UnofficialStatus>(command.Status);
		await _context.SaveChangesAsync();

		return $"ArrangementSong [{command.Id}] was {command.Status} successfully.";
	}
}
