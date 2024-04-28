using Touhou_Songs.App.Unofficial.Songs;

namespace Touhou_Songs.App.Unofficial.Circles;

public class Circle
{
	public int Id { get; set; }
	public string Name { get; set; }

	// "Pending", "Confirmed", "Rejected"
	public UnofficialStatus Status { get; set; }

	public required List<ArrangementSong> ArrangementSongs { get; set; }

	public Circle(string name, UnofficialStatus status) => (Name, Status) = (name, status);
}
