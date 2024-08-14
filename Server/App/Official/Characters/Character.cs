using System.Diagnostics;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.Infrastructure.BaseEntities;

namespace Touhou_Songs.App.Official.Characters;

[DebuggerDisplay("{Name}")]
public class Character : BaseAuditedEntity
{
	public string Name { get; set; }
	public string ImageUrl { get; set; }

	public required int OriginGameId { get; set; }
	public required OfficialGame OriginGame { get; set; }
	public required List<OfficialSong> OfficialSongs { get; set; }

	public Character(string name, string imageUrl)
		=> (Name, ImageUrl) = (name, imageUrl);
}
