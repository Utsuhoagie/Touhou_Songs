using Testcontainers.PostgreSql;

namespace Testing;

public sealed class PostgreSqlTestContainer : IAsyncLifetime
{
	public PostgreSqlContainer PostgreSql { get; } = new PostgreSqlBuilder()
		.WithImage("postgres:15")
		.Build();

	public Task InitializeAsync() => PostgreSql.StartAsync();

	public Task DisposeAsync() => PostgreSql.DisposeAsync().AsTask();
}