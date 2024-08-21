using System.Net;

namespace Touhou_Songs.Infrastructure.ExceptionHandling;

/// <summary>
/// Unused!!!
/// </summary>
public class AppException : Exception
{
	public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;
	public object? Value { get; }

	public AppException(HttpStatusCode statusCode, object? value = null) => (StatusCode, Value) = (statusCode, value);
}
