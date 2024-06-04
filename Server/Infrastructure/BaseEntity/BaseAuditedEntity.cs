namespace Touhou_Songs.Infrastructure.BaseEntity;

public abstract class BaseAuditedEntity : BaseEntity
{
	public DateTime CreatedOn { get; set; }
	public string CreatedByUserName { get; set; } = string.Empty;

	public DateTime? UpdatedOn { get; set; }
	public string? UpdatedByUserName { get; set; }
}
