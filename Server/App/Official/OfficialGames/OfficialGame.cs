using System.Diagnostics;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.Infrastructure.BaseEntity;

namespace Touhou_Songs.App.Official.OfficialGames;

[DebuggerDisplay("[{GameCode}] {Title}")]
public class OfficialGame : BaseAuditedEntity
{
	public string Title { get; set; }
	public string GameCode { get; set; }
	public string NumberCode { get; set; }
	public DateTime ReleaseDate { get; set; }
	public string ImageUrl { get; set; }

	public required List<OfficialSong> Songs { get; set; }

	public OfficialGame(string title, string gameCode, string numberCode, DateTime releaseDate, string imageUrl)
		=> (Title, GameCode, NumberCode, ReleaseDate, ImageUrl) = (title, gameCode, numberCode, releaseDate, imageUrl);
}
