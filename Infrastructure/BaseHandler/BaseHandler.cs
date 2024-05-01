using System.Net;
using MediatR;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.Infrastructure.BaseHandler;

public class Result<T>
{
	public bool Success { get; init; } = false;
	public HttpStatusCode StatusCode { get; init; }
	public List<string> Messages { get; init; } = new();

	public T? Value { get; init; }
}

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	protected readonly AuthUtils _authUtils;
	protected readonly Touhou_Songs_Context _context;

	public BaseHandler(AuthUtils authUtils, Touhou_Songs_Context context)
		=> (_authUtils, _context) = (authUtils, context);

	public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public abstract class BaseHandler<TRequest, TResponse, TResultWrapper> : IRequestHandler<TRequest, TResultWrapper>
	where TRequest : IRequest<TResultWrapper>
	where TResultWrapper : Result<TResponse>
{
	protected readonly AuthUtils _authUtils;
	protected readonly Touhou_Songs_Context _context;

	public BaseHandler(AuthUtils authUtils, Touhou_Songs_Context context)
		=> (_authUtils, _context) = (authUtils, context);

	public abstract Task<TResultWrapper> Handle(TRequest request, CancellationToken cancellationToken);

	public Result<TResponse> Ok(TResponse? value, string? message = null, IEnumerable<string>? messages = null) => new()
	{
		Value = value,
		Success = true,
		StatusCode = HttpStatusCode.OK,
		Messages = ConcatMessages(message, messages),
	};

	public Result<TResponse> BadRequest(string? message = null, IEnumerable<string>? messages = null) => new()
	{
		StatusCode = HttpStatusCode.BadRequest,
		Messages = ConcatMessages(message, messages),
	};

	public Result<TResponse> Unauthorized(string? message = null, IEnumerable<string>? messages = null) => new()
	{
		StatusCode = HttpStatusCode.Unauthorized,
		Messages = ConcatMessages(message, messages),
	};

	public Result<TResponse> Forbidden(string? message = null, IEnumerable<string>? messages = null) => new()
	{
		StatusCode = HttpStatusCode.Forbidden,
		Messages = ConcatMessages(message, messages),
	};

	public Result<TResponse> NotFound(string? message = null, IEnumerable<string>? messages = null) => new()
	{
		StatusCode = HttpStatusCode.NotFound,
		Messages = ConcatMessages(message, messages),
	};

	public Result<TResponse> Conflict(string? message = null, IEnumerable<string>? messages = null) => new()
	{
		StatusCode = HttpStatusCode.Conflict,
		Messages = ConcatMessages(message, messages),
	};

	private List<string> ConcatMessages(string? message, IEnumerable<string>? messages)
	{
		var messagesList = (messages ?? new List<string>()).ToList();

		if (message is not null)
		{
			messagesList.Add(message);
		}

		return messagesList;
	}
}