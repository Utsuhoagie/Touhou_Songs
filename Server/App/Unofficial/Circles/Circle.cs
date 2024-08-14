using System.Diagnostics;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Infrastructure.BaseEntities;

namespace Touhou_Songs.App.Unofficial.Circles;

[DebuggerDisplay("{Name} - {Status}")]
public class Circle : BaseAuditedEntity
{
	public string Name { get; set; }

	public UnofficialStatus Status { get; set; }

	public required List<ArrangementSong> ArrangementSongs { get; set; }

	public Circle(string name, UnofficialStatus status) => (Name, Status) = (name, status);
}
