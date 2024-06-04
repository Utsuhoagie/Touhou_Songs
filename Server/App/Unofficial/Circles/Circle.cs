using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Infrastructure.BaseEntity;

namespace Touhou_Songs.App.Unofficial.Circles;

public class Circle : BaseAuditedEntity
{
	public string Name { get; set; }

	public UnofficialStatus Status { get; set; }

	public required List<ArrangementSong> ArrangementSongs { get; set; }

	public Circle(string name, UnofficialStatus status) => (Name, Status) = (name, status);
}
