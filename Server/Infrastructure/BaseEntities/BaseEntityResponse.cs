namespace Touhou_Songs.Infrastructure.BaseEntities;

public record BaseEntityResponse
{
	public int? Id { get; set; }

	public BaseEntityResponse() { }
	public BaseEntityResponse(BaseEntity entity) => (Id) = (entity.Id);
}
