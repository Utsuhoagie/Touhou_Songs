namespace Touhou_Songs.Infrastructure.BaseEntity;

public abstract record BaseEntityResponse
{
	public int Id { get; set; }

	public BaseEntityResponse(BaseEntity entity) => Id = entity.Id;
}
