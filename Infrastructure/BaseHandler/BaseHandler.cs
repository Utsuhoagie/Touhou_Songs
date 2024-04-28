﻿using MediatR;
using Touhou_Songs.Data;

namespace Touhou_Songs.Infrastructure.BaseHandler;

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	protected readonly IHttpContextAccessor _httpContextAccessor;
	protected readonly Touhou_Songs_Context _context;

	public BaseHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context)
		=> (_httpContextAccessor, _context) = (httpContextAccessor, context);

	public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
