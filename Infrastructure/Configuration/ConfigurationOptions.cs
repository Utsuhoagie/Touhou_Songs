namespace Touhou_Songs.Infrastructure.Configuration;

public class ConfigurationOptions
{
	public ConnectionStringOptions ConnectionString { get; set; } = new();
	public class ConnectionStringOptions
	{
		public string Touhou_Songs { get; set; } = string.Empty;
	}

	public JWTOptions JWT { get; set; } = new();
	public class JWTOptions
	{
		public string ValidAudience { get; set; } = string.Empty;
		public string ValidIssuer { get; set; } = string.Empty;
		public int ValidHours { get; set; }
		public string Secret { get; set; } = string.Empty;
	}
}