using Touhou_Songs.Features.Official.OfficialGames;

namespace Touhou_Songs.Features.Official.OfficialSongs
{
	public class OfficialSong
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Origin { get; set; } = string.Empty;

		public int GameId { get; set; }
		public OfficialGame Game { get; set; } = new();
	}
}
