using System.Diagnostics;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.App.Unofficial.Songs;

namespace Touhou_Songs.App._JoinEntities;

[DebuggerDisplay("{OfficialSong.Title} - {ArrangementSong.Title}")]
public class OfficialSongArrangementSong
{
	public int Id { get; set; }
	public required int OfficialSongId { get; set; }
	public required OfficialSong OfficialSong { get; set; }
	public required int ArrangementSongId { get; set; }
	public required ArrangementSong ArrangementSong { get; set; }
}
