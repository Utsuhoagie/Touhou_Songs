using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionString:Touhou_Songs_Context"] ?? throw new InvalidOperationException("Connection string 'Touhou_Songs_Context' not found.");

builder.Services.AddDbContext<Touhou_Songs_Context>(options =>
	options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
