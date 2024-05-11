using Testing;
using Testing.Integration;

namespace Touhou_Songs.Tests;

public class GameIntegrationTest : BaseIntegrationTest
{
	public GameIntegrationTest(PostgreSqlTestContainer fixture) : base(fixture) { }

	[Fact]
	public async Task Test_Get_OfficialGames()
	{
		// Arrange
		HttpClient.DefaultRequestHeaders.Authorization = new(
			"Bearer",
			"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBleGFtcGxlLmNvbSIsImp0aSI6ImQ1MDQ3YzZkLTU3NzUtNDdjNi1hMGEzLTMyOTMyMTE5NDE0MyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzE1NDM3MTA5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDozMDAwIn0.MHebpveyjjAfJ3bz_G7UWFAMvhpiqMOKUG_IQ7D0S-o");

		// Act
		var res_http = await HttpClient.GetAsync("/api/OfficialGames");

		var message = await res_http.Content.ReadAsStringAsync();
		res_http.EnsureSuccessStatusCode();
	}
}