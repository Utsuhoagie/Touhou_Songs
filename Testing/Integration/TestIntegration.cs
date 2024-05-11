using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.PostgreSql;
using Touhou_Songs.Data;

namespace Touhou_Songs.Tests;


public sealed class PostgreSqlTestContainer : IAsyncLifetime
{
	public PostgreSqlContainer PostgreSql { get; } = new PostgreSqlBuilder()
		.WithImage("postgres:15")
		.Build();

	public Task InitializeAsync() => PostgreSql.StartAsync();

	public Task DisposeAsync() => PostgreSql.DisposeAsync().AsTask();
}

sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	private readonly string _connectionString;

	public CustomWebApplicationFactory(PostgreSqlTestContainer fixture)
	{
		_connectionString = fixture.PostgreSql.GetConnectionString();
	}

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			services.Remove(services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<Touhou_Songs_Context>))!);
			services.Remove(services.SingleOrDefault(service => service.ServiceType == typeof(DbConnection))!);

			services.AddDbContext<Touhou_Songs_Context>((_, option) => option.UseNpgsql(_connectionString));
		});
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		builder.UseContentRoot(Directory.GetCurrentDirectory());
		return base.CreateHost(builder);
	}
}

public abstract class BaseIntegrationTest : IClassFixture<PostgreSqlTestContainer>, IDisposable
{
	private readonly WebApplicationFactory<Program> _webAppFactory;
	private readonly IServiceScope _scope;
	protected readonly HttpClient HttpClient;
	protected readonly Touhou_Songs_Context Context;

	public BaseIntegrationTest(PostgreSqlTestContainer fixture)
	{
		_webAppFactory = new CustomWebApplicationFactory(fixture);

		_scope = _webAppFactory.Services.CreateScope();

		var clientOptions = new WebApplicationFactoryClientOptions();
		clientOptions.AllowAutoRedirect = false;
		HttpClient = _webAppFactory.CreateClient(clientOptions);

		Context = _scope.ServiceProvider.GetRequiredService<Touhou_Songs_Context>();
		Context.Database.Migrate();
	}

	public void Dispose() => _webAppFactory.Dispose();
}

public class GameIntegrationTest : BaseIntegrationTest
{
	public GameIntegrationTest(PostgreSqlTestContainer fixture) : base(fixture) { }

	[Fact]
	public async Task Test_Get_OfficialGame_Detail()
	{
		// Arrange
		var gameCode = "SA";
		HttpClient.DefaultRequestHeaders.Authorization = new(
			"Bearer",
			"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBleGFtcGxlLmNvbSIsImp0aSI6ImQ1MDQ3YzZkLTU3NzUtNDdjNi1hMGEzLTMyOTMyMTE5NDE0MyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzE1NDM3MTA5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDozMDAwIn0.MHebpveyjjAfJ3bz_G7UWFAMvhpiqMOKUG_IQ7D0S-o");

		// Act
		var res = await HttpClient.GetAsync($"/api/OfficialGames/{gameCode}");

		res.EnsureSuccessStatusCode();
	}
}