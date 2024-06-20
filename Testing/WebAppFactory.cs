using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Touhou_Songs.Data;

namespace Testing;

sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	private readonly string _connectionString;

	public CustomWebApplicationFactory(PostgreSqlTestContainer fixture) => _connectionString = fixture.PostgreSql.GetConnectionString();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureTestServices(services =>
		{
			services.Remove(services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<AppDbContext>))!);
			services.Remove(services.SingleOrDefault(service => service.ServiceType == typeof(DbConnection))!);

			services.AddDbContext<AppDbContext>((_, option) => option.UseNpgsql(_connectionString));

			var serviceProvider = services.BuildServiceProvider();

			using var scope = serviceProvider.CreateScope();
			var scopedServices = scope.ServiceProvider;
			var context = scopedServices.GetRequiredService<AppDbContext>();
			context.Database.EnsureCreated();
			//context.Database
		});
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		builder.UseContentRoot(Directory.GetCurrentDirectory());
		return base.CreateHost(builder);
	}
}