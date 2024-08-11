using Touhou_Songs.Infrastructure.BaseEntity;

namespace Touhou_Songs.App.TierListMaking;

/// <summary>
/// [ArrangementSongs, OfficialGames]
/// </summary>
public enum TierListType
{
	ArrangementSongs,
	OfficialGames,
}

public class TierList : BaseAuditedEntity
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public TierListType Type { get; set; }

	public List<TierListTier> Tiers { get; set; } = new();

	public TierList(string title, string? description, TierListType type)
		=> (Title, Description, Type) = (title, description, type);
}

public class TierListTier : BaseEntity
{
	public required int TierListId { get; set; }
	public required TierList TierList { get; set; }

	public string Label { get; set; }
	public int Order { get; set; }

	public required List<TierListItem> Items { get; set; }

	public TierListTier(string label, int order) => (Label, Order) = (label, order);
}

public class TierListItem : BaseEntity
{
	public int TierListTierId { get; set; }
	public TierListTier TierListTier { get; set; } = default!;

	//public string Label => Source.GetLabel();
	public int Order { get; set; }
	public string IconUrl { get; set; }

	//public required int SourceId { get; set; }
	//public required BaseAuditedEntity Source { get; set; }

	public TierListItem(int order, string iconUrl) => (Order, IconUrl) = (order, iconUrl);
}