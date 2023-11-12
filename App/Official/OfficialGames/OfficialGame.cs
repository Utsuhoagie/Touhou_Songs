using Touhou_Songs.App.Official.OfficialSongs;

namespace Touhou_Songs.App.Official.OfficialGames
{
	public class OfficialGame
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string GameCode { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string ImageUrl { get; set; }

		public List<OfficialSong> Songs { get; set; } = new();

		public OfficialGame(string title, string gameCode, DateTime releaseDate, string imageUrl)
			=> (Title, GameCode, ReleaseDate, ImageUrl) = (title, gameCode, releaseDate, imageUrl);
	}
}
