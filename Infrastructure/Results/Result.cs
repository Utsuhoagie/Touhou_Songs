using System.Net;

namespace Touhou_Songs.Infrastructure.Results;

public class Result<T>
{
	public bool Success { get; init; } = false;
	public HttpStatusCode StatusCode { get; init; }
	public List<string> Messages { get; init; } = new();

	public T? Value { get; init; }
}
