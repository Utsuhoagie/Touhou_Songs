namespace Touhou_Songs.Infrastructure.BaseEntities;

public abstract class BaseAuditedEntity : BaseEntity
{
	public DateTime CreatedOn { get; set; }
	public string CreatedByUserName { get; set; } = string.Empty;

	public DateTime? UpdatedOn { get; set; }
	public string? UpdatedByUserName { get; set; }
}
