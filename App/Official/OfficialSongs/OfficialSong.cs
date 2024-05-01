using Touhou_Songs.App._JoinEntities;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.App.Unofficial.Songs;

namespace Touhou_Songs.App.Official.OfficialSongs;

public class OfficialSong
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Context { get; set; }

	public required int GameId { get; set; }
	public required OfficialGame Game { get; set; }
	public required List<Character> Characters { get; set; }

	public required List<ArrangementSong> ArrangementSongs { get; set; }
	public required List<OfficialSongArrangementSong> OfficialSongArrangementSongs { get; set; }

	public OfficialSong(string title, string context)
		=> (Title, Context) = (title, context);
}
