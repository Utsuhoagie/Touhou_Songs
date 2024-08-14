namespace Touhou_Songs.Infrastructure.Results;

public record FileResponse
{
	public byte[] Contents { get; set; } = [];
	public string MimeType { get; set; } = string.Empty;
	public string FileName { get; set; } = string.Empty;
}