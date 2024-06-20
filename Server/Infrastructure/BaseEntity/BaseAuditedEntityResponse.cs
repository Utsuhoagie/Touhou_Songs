﻿namespace Touhou_Songs.Infrastructure.BaseEntity;

public abstract record BaseAuditedEntityResponse : BaseEntityResponse
{
	public DateTime CreatedOn { get; set; }
	public string CreatedByUserName { get; set; } = string.Empty;

	public DateTime? UpdatedOn { get; set; }
	public string? UpdatedByUserName { get; set; }

	public BaseAuditedEntityResponse(BaseAuditedEntity entity) : base(entity)
		=> (CreatedOn, CreatedByUserName, UpdatedOn, UpdatedByUserName)
		= (entity.CreatedOn, entity.CreatedByUserName, entity.UpdatedOn, entity.UpdatedByUserName);
}
