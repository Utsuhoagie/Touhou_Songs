using System.Net;

namespace Touhou_Songs.Infrastructure.ExceptionHandling
{
	public class AppException : Exception
	{
		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
		public object? Value { get; }

		public AppException(HttpStatusCode statusCode, object? value = null) => (StatusCode, Value) = (statusCode, value);
	}
}
