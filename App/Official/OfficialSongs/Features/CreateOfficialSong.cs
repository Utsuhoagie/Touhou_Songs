using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Official.OfficialSongs.Features
{
	public record CreateOfficialSongCommand : IRequest
	{
		public string Title { get; set; }
		public string Context { get; set; }

		public string GameCode { get; set; }

		public CreateOfficialSongCommand(string title, string context, string gameCode)
			=> (Title, Context, GameCode) = (title, context, gameCode);
	}

	class CreateOfficialSongCommandHandler : IRequestHandler<CreateOfficialSongCommand>
	{
		private readonly Touhou_Songs_Context _context;

		public CreateOfficialSongCommandHandler(Touhou_Songs_Context context) => _context = context;

		public async Task Handle(CreateOfficialSongCommand command, CancellationToken cancellationToken)
		{
			var officialGame = await _context.OfficialGames
				.SingleOrDefaultAsync(og => EF.Functions.ILike(og.GameCode, $"{command.GameCode}"));

			if (officialGame is null)
			{
				throw new AppException(HttpStatusCode.NotFound, $"Official Game {command.GameCode} not found");
			}

			var officialSong = new OfficialSong(command.Title, command.Context, officialGame.Id)
			{
				Game = officialGame,
			};

			_context.OfficialSongs.Add(officialSong);
			await _context.SaveChangesAsync();
		}
	}
}
