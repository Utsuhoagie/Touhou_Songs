namespace Touhou_Songs.Infrastructure.BaseEntity;

public record BaseEntityResponse
{
	public int? Id { get; set; }

	public BaseEntityResponse() { }
	public BaseEntityResponse(BaseEntity entity) => (Id) = (entity.Id);
}
