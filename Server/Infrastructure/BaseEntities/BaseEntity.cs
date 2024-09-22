namespace Touhou_Songs.Infrastructure.BaseEntities;

public abstract class BaseEntity
{
	public int Id { get; set; }

	public virtual string GetIdForRoute() => $"{Id}";

	public virtual string? GetImageUrl() => null;

	public string GetLabel()
	{
		var type = GetType();
		var propertyNamesForLabel = new[] { "Name", "Title" };

		foreach (var propertyName in propertyNamesForLabel)
		{
			var propertyValue = type.GetProperty(propertyName)?.GetValue(this);

			if (propertyValue is not null)
			{
				return $"{propertyValue}";
			}
		}

		var commaSeparatedPropertyNames = string.Join("/", propertyNamesForLabel);
		return $"No {commaSeparatedPropertyNames} was found for this entity";

		//var name = type.GetProperty("Name", typeof(string))?.GetValue(this)?.ToString();
		//var title = type.GetProperty("Title", typeof(string))?.GetValue(this)?.ToString();

		//return name ?? title ?? "NO NAME OR TITLE FOUND";
	}
}
