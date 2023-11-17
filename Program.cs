using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.ExceptionHandling;

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

	builder.Services.AddCors(o => o.AddPolicy("My_CORS_Policy", p =>
		p.WithOrigins("http://localhost:3000")));

	builder.Services.AddDbContext<Touhou_Songs_Context>(options => options.UseNpgsql(connectionString));

	builder.Services
		.AddIdentity<AppUser, IdentityRole>()
		.AddEntityFrameworkStores<Touhou_Songs_Context>();

	builder.Services
		.Configure<IdentityOptions>(options =>
		{
			options.Password.RequireLowercase = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireDigit = false;
		});

	builder.Services
		.AddAuthentication(options =>
		{
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})

		// Adding Jwt Bearer
		.AddJwtBearer(options =>
		{
			options.SaveToken = true;
			options.RequireHttpsMetadata = false;
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidAudience = builder.Configuration["JWT:ValidAudience"],
				ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
			};
		});

	builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

	builder.Services
		.AddControllers(o => o.Filters.Add<AppExceptionFilter>())
		.AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = null);

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

	app.UseAuthentication();
	app.UseAuthorization();

	app.UseCors("My_CORS_Policy");

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