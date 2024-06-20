using MediatR;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.Infrastructure.BaseHandler;

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
	where TRequest : IRequest<Result<TResponse>>
{
	protected readonly AuthUtils _authUtils;
	protected readonly AppDbContext _context;
	protected readonly ResultFactory<TResponse> _resultFactory = new();

	public BaseHandler(AuthUtils authUtils, AppDbContext context)
		=> (_authUtils, _context) = (authUtils, context);

	public abstract Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}