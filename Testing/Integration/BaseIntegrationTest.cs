using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Touhou_Songs.Data;

namespace Testing.Integration;

public abstract class BaseIntegrationTest : IClassFixture<PostgreSqlTestContainer>, IDisposable
{
	private readonly WebApplicationFactory<Program> _webAppFactory;
	protected readonly HttpClient HttpClient;
	protected readonly Touhou_Songs_Context Context;

	public BaseIntegrationTest(PostgreSqlTestContainer fixture)
	{
		_webAppFactory = new CustomWebApplicationFactory(fixture);

		var clientOptions = new WebApplicationFactoryClientOptions();
		clientOptions.AllowAutoRedirect = false;

		HttpClient = _webAppFactory.CreateClient(clientOptions);

		using var scope = _webAppFactory.Services.CreateScope();
		Context = scope.ServiceProvider.GetRequiredService<Touhou_Songs_Context>();
	}

	public void Dispose() => _webAppFactory.Dispose();

	protected async Task LoginAs(string role)
	{

	}
}