using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.App.Unofficial.Songs;

namespace Touhou_Songs.App.Official._JoinEntities;

public class OfficialSongArrangementSong
{
	public int Id { get; set; }
	public required int OfficialSongId { get; set; }
	public required OfficialSong OfficialSong { get; set; }
	public required int ArrangementSongId { get; set; }
	public required ArrangementSong ArrangementSong { get; set; }
}
