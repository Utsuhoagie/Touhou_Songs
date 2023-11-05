using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		[HttpPost]
		public async Task<IActionResult> ImportSongs([FromBody] List<ImportSongsOfGameCommand> allSongs)
		{
			var allGames = await _context.OfficialGames.ToListAsync();
			var newSongs = new List<OfficialSong>();

			try
			{
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
						};

						newSongs.Add(newSong);
					}
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			//_context.OfficialSongs.AddRange(newSongs);
			//await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
