using Touhou_Songs.Features.Official.OfficialSongs;

namespace Touhou_Songs.Features.Official.OfficialGames
{
	public class OfficialGame
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public DateTime ReleaseDate { get; set; }
		public string ImageUrl { get; set; } = string.Empty;

		public List<OfficialSong> Songs { get; set; } = new();
	}
}
