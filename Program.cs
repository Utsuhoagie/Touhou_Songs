using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using Touhou_Songs.Data;

// NOTE
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionString:Touhou_Songs"] ?? throw new InvalidOperationException("Connection string 'Touhou_Songs_Context' not found.");

try
{
	// Add services to the container.
	builder.Host.UseSerilog(
	(context, services, configuration) =>
		configuration
			.ReadFrom.Configuration(context.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext()
			.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u5}] {Message:lj}{NewLine}{Exception}")
	);

	builder.Services.AddDbContext<Touhou_Songs_Context>(options => options.UseNpgsql(connectionString));

	builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

	builder.Services.AddControllers();

	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services
		.AddEndpointsApiExplorer()
		.AddSwaggerGen(o => o.DocumentFilter<ImportableDocumentFilter>());

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
catch (Exception ex)
{
	if (ex is HostAbortedException)
	{
		return;
	}
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}


public class ImportableDocumentFilter : IDocumentFilter
{
	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
	{
		var controllers = Assembly
			.GetExecutingAssembly()
			.GetTypes()
			.Where(x => x.GetCustomAttribute<ApiControllerAttribute>() is not null);

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