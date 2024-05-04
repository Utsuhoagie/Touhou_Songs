using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Touhou_Songs.Infrastructure.API;

public class ImportableDocumentFilter : IDocumentFilter
{
	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
	{
		var controllers = Assembly
			.GetExecutingAssembly()
			.GetTypes()
			.Where(type =>
				!type.IsAbstract &&
				type.GetCustomAttribute<ApiControllerAttribute>() is not null);

		swaggerDoc.Tags = controllers
			.Select(controller => new OpenApiTag
			{
				Name = controller.Name.Replace("Controller", string.Empty)
			})
			.OrderBy(t => t.Name)
			.ToList();

		swaggerDoc.Servers = new List<OpenApiServer>
		{
			new OpenApiServer
			{
				Url = "https://localhost:5000",
			}
		};
	}
}