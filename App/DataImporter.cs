using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.Data;

namespace Touhou_Songs.App
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataImporterController : ControllerBase
	{
		private readonly Touhou_Songs_Context _context;

		public DataImporterController(Touhou_Songs_Context context) => _context = context;

		public class ImportSongsOfGameCommand
		{
			public string GameCode { get; set; }
			public List<string> Songs { get; set; }
		}

		[HttpPost("OfficialSongs")]
		public async Task<IActionResult> ImportSongs([FromBody] List<ImportSongsOfGameCommand> allSongs)
		{
			var allGames = await _context.OfficialGames.ToListAsync();
			var newSongs = new List<OfficialSong>();

			foreach (var gameSongs in allSongs)
			{
				var game = allGames.SingleOrDefault(og => og.GameCode == gameSongs.GameCode);

				if (game is null)
				{
					return NotFound($"Game {gameSongs.GameCode} not found");
				}

				foreach (var song in gameSongs.Songs)
				{
					var newSong = new OfficialSong(song, "??", game.Id)
					{
						Game = game,
						Characters = new(), // PLACEHOLDER!!!!!!!!
					};

					newSongs.Add(newSong);
				}
			}

			//_context.OfficialSongs.AddRange(newSongs);
			//await _context.SaveChangesAsync();

			return Ok();
		}

		public class ImportCharacterCommand
		{
			public string Name { get; set; }
			public string ImageUrl { get; set; }

			public string GameCode { get; set; }
			public List<string> Songs { get; set; }
		}

		[HttpPost("Characters")]
		public async Task<IActionResult> ImportCharacters([FromBody] List<ImportCharacterCommand> importCharacters)
		{
			var importGameCodes = importCharacters.Select(ic => ic.GameCode);

			var games = await _context.OfficialGames
				.Where(og => importGameCodes.Contains(og.GameCode))
				.ToListAsync();

			var importSongsByCharacter = importCharacters
				.Select(ic => new
				{
					Character = ic.Name,
					Songs = ic.Songs,
				});

			var importSongTitles = importSongsByCharacter
				.SelectMany(isbc => isbc.Songs);

			var songs = await _context.OfficialSongs
				.Where(os => importSongTitles.Contains(os.Title))
				.ToListAsync();

			var newCharacters = new List<Character>();

			foreach (var importCharacter in importCharacters)
			{
				var originGame = games.SingleOrDefault(og => og.GameCode == importCharacter.GameCode);

				if (originGame is null)
				{
					continue;
				}

				var characterSongTitles = importSongsByCharacter
					.SingleOrDefault(isbc => isbc.Character == importCharacter.Name)
					!
					.Songs;

				var characterSongs = songs
					.Where(os => characterSongTitles.Contains(os.Title))
					.ToList();

				var character = new Character(importCharacter.Name, importCharacter.ImageUrl, originGame.Id)
				{
					OriginGame = originGame,
					OfficialSongs = characterSongs,
				};

				newCharacters.Add(character);
			}

			_context.Characters.AddRange(newCharacters);
			await _context.SaveChangesAsync();

			return Ok();
		}



		public class UpdateSongContextCommand
		{
			public int Id { get; set; }
			public string Title { get; set; }
			public string GameCode { get; set; }

			// NOTE: This is NEW context!
			public string Context { get; set; }

			public UpdateSongContextCommand(int id, string title, string gameCode, string context)
				=> (Id, Title, GameCode, Context) = (id, title, gameCode, context);
		}

		[HttpPut("SongsContext")]
		public async Task<IActionResult> UpdateSongsContext([FromBody] List<UpdateSongContextCommand> updateSongContexts)
		{
			var songIds = updateSongContexts.Select(u => u.Id);
			var songsToUpdate = await _context.OfficialSongs
				.Where(os => songIds.Contains(os.Id))
				.ToListAsync();

			foreach (var song in songsToUpdate)
			{
				var updatedSong = updateSongContexts.SingleOrDefault(u => u.Id == song.Id);

				song.Context = updatedSong!.Context;
			}

			await _context.SaveChangesAsync();

			return Ok();
		}



		public class UpdateCharacterImageUrl
		{
			public string Name { get; set; }
			public string ImageUrl { get; set; }
			public string GameCode { get; set; }
			public List<string> Songs { get; set; }
		}

		[HttpPut("CharactersImageUrl")]
		public async Task<IActionResult> UpdateCharactersImageUrl([FromBody] List<UpdateCharacterImageUrl> updateCharacters)
		{
			var characterNames = updateCharacters.Select(u => u.Name);
			var charactersToUpdate = await _context.Characters
				.Include(c => c.OfficialSongs)
				.Where(c => characterNames.Contains(c.Name))
				.ToListAsync();

			foreach (var character in charactersToUpdate)
			{
				var updatedCharacter = updateCharacters.SingleOrDefault(u => u.Name == character.Name);

				character.ImageUrl = updatedCharacter!.ImageUrl;
			}

			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}