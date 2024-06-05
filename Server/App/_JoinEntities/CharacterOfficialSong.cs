﻿using System.Diagnostics;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialSongs;

namespace Touhou_Songs.App._JoinEntities;

[DebuggerDisplay("{Character.Name} - {OfficialSong.Title}")]
public class CharacterOfficialSong
{
	public int Id { get; set; }
	public required int CharacterId { get; set; }
	public required Character Character { get; set; }
	public required int OfficialSongId { get; set; }
	public required OfficialSong OfficialSong { get; set; }
}
