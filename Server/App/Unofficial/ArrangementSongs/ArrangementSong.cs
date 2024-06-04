using Touhou_Songs.App._JoinEntities;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.App.Unofficial.Circles;
using Touhou_Songs.Infrastructure.BaseEntity;

namespace Touhou_Songs.App.Unofficial.Songs;

public class ArrangementSong : BaseAuditedEntity
{
	public string Title { get; set; }
	public string? TitleRomaji { get; set; }
	public string? TitleJapanese { get; set; }
	public string Url { get; set; }

	public UnofficialStatus Status { get; set; }

	public required int CircleId { get; set; }
	public required Circle Circle { get; set; }

	public required List<OfficialSong> OfficialSongs { get; set; }
	public required List<OfficialSongArrangementSong> OfficialSongArrangementSongs { get; set; }

	public ArrangementSong(string title, string url, UnofficialStatus status)
		=> (Title, Url, Status) = (title, url, status);
}
