using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialGames;

namespace Touhou_Songs.App.Official.OfficialSongs
{
	public class OfficialSong
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Context { get; set; }

		public int GameId { get; set; }
		public required OfficialGame Game { get; set; }
		public required List<Character> Characters { get; set; }

		public OfficialSong(string title, string context, int gameId)
			=> (Title, Context, GameId) = (title, context, gameId);

	}
}
