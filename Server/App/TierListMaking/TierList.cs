using Touhou_Songs.Infrastructure.BaseEntity;

namespace Touhou_Songs.App.TierListMaking;

public enum TierListType
{
	ArrangementSongs,
}

public class TierList : BaseAuditedEntity
{
	public int Id { get; set; }

	public string Title { get; set; }
	public string? Description { get; set; }
	public TierListType Type { get; set; }

	public List<TierListTier> Tiers { get; set; } = new();

	public TierList(string title, string? description, TierListType type)
		=> (Title, Description, Type) = (title, description, type);
}

public class TierListTier
{
	public int Id { get; set; }

	public string Label { get; set; }
	public int Order { get; set; }

	public List<TierListItem> Items { get; set; } = new();

	public TierListTier(string label, int order) => (Label, Order) = (label, order);
}

public class TierListItem
{
	public int Id { get; set; }

	public string Label { get; set; }
	public int Order { get; set; }
	public string IconUrl { get; set; }

	public TierListItem(string label, int order, string iconUrl) => (Label, Order, IconUrl) = (label, order, iconUrl);
}