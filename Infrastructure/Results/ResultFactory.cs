﻿using System.Net;

namespace Touhou_Songs.Infrastructure.Results;

public class ResultFactory<TResponse>
{
	public Result<TResponse> FromResult<T>(Result<T> result, string? message = null, IEnumerable<string>? messages = null) => new()
	{
		Value = default,
		Success = result.Success,
		StatusCode = result.StatusCode,
		Messages = ConcatMessages(message, messages),
	};

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
