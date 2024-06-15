using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Touhou_Songs.App.Unofficial.Circles.Features;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.API;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.Configuration;
using Touhou_Songs.Infrastructure.ExceptionHandling;

// NOTE
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog(
(context, services, configuration) =>
	configuration
		.ReadFrom.Configuration(context.Configuration)
		.ReadFrom.Services(services)
		.Enrich.FromLogContext()
		.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u5}] {Message:lj}{NewLine}{Exception}")
		.WriteTo.File(
			path: ".\\Logs\\log.txt",
			outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u5}] {Message:lj}{NewLine}{Exception}")
);



var connectionString = builder.Configuration["ConnectionString:Touhou_Songs"] ?? throw new InvalidOperationException("Connection string 'Touhou_Songs_Context' not found.");

builder.Services.AddCors(o => o.AddPolicy("My_CORS_Policy", p => p
	.WithOrigins("http://localhost:3000")
	.AllowAnyHeader()
	.AllowAnyMethod()));

builder.Services.AddScoped<AuthUtils>();

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

builder.Services.AddDbContext<Touhou_Songs_Context>(options => options
	.UseNpgsql(connectionString)
	.UseSnakeCaseNamingConvention());

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
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				builder.Configuration["JWT:Secret"]
				?? throw new AppException(HttpStatusCode.InternalServerError, "JWT Secret not found."))),
		};
	});

builder.Services.Configure<ConfigurationOptions>(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services
	.AddControllers(o => o.Filters.Add<AppExceptionFilter>())
	.AddJsonOptions(o =>
	{
		o.JsonSerializerOptions.PropertyNamingPolicy = null;
		o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

builder.Services
	.AddValidatorsFromAssemblyContaining<CreateCircleValidator>();

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

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("My_CORS_Policy");

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }