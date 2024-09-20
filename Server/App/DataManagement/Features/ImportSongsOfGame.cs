using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.DataManagement.Features;

public record ImportSongsOfGamesCommand : IRequest<Result<string>>
{
	public List<ImportSongsOfGame> GameSongs { get; set; } = new();

	public record ImportSongsOfGame
	{
		public string GameCode { get; set; } = string.Empty;
		public List<string> Songs { get; set; } = new();
	}
}

class ImportSongsOfGameHandler : BaseHandler<ImportSongsOfGamesCommand, string>
{
	public ImportSongsOfGameHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public async override Task<Result<string>> Handle(ImportSongsOfGamesCommand command, CancellationToken cancellationToken)
	{
		var allGames = await _context.OfficialGames.ToListAsync();
		var newSongs = new List<OfficialSong>();

		foreach (var gameSongs in command.GameSongs)
		{
			var game = allGames.SingleOrDefault(og => og.GameCode == gameSongs.GameCode);

			if (game is null)
			{
				return _resultFactory.NotFound($"Game {gameSongs.GameCode} not found");
			}

			foreach (var song in gameSongs.Songs)
			{
				var newSong = new OfficialSong(song, "??")
				{
					GameId = game.Id,
					Game = game,
					Characters = new(), // PLACEHOLDER!!!!!!!!
					CharacterOfficialSongs = new(),
					ArrangementSongs = new(), // PLACEHOLDER!!!!!
					OfficialSongArrangementSongs = new(),
				};

				newSongs.Add(newSong);
			}
		}

		//_context.OfficialSongs.AddRange(newSongs);
		//await _context.SaveChangesAsync();

		return _resultFactory.Ok(null);
	}
}