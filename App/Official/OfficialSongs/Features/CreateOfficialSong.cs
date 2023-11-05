using System.Net;
using MediatR;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Official.OfficialSongs.Features
{
	public record CreateOfficialSongCommand : IRequest
	{
		public string Title { get; set; }
		public string Context { get; set; }

		public int GameId { get; set; }

		public CreateOfficialSongCommand(string title, string context, int gameId)
			=> (Title, Context, GameId) = (title, context, gameId);
	}

	class CreateOfficialSongCommandHandler : IRequestHandler<CreateOfficialSongCommand>
	{
		private readonly Touhou_Songs_Context _context;

		public CreateOfficialSongCommandHandler(Touhou_Songs_Context context) => _context = context;

		public async Task Handle(CreateOfficialSongCommand command, CancellationToken cancellationToken)
		{
			var officialGame = await _context.OfficialGames.FindAsync(command.GameId);

			if (officialGame is null)
			{
				throw new AppException(HttpStatusCode.NotFound, $"Official Game {command.GameId} not found");
			}

			var officialSong = new OfficialSong(command.Title, command.Context, command.GameId)
			{
				Game = officialGame,
			};

			_context.OfficialSongs.Add(officialSong);
			await _context.SaveChangesAsync();
		}
	}
}
