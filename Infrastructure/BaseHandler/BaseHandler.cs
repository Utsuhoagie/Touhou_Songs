using MediatR;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.Infrastructure.BaseHandler;

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	protected readonly AuthUtils _authUtils;
	protected readonly Touhou_Songs_Context _context;

	public BaseHandler(AuthUtils authUtils, Touhou_Songs_Context context)
		=> (_authUtils, _context) = (authUtils, context);

	public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
