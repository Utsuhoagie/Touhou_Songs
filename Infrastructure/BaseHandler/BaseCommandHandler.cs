using Touhou_Songs.Data;

namespace Touhou_Songs.Infrastructure.BaseHandler
{
	public abstract class BaseHandler
	{
		protected readonly IHttpContextAccessor _httpContextAccessor;
		protected readonly Touhou_Songs_Context _context;

		public BaseHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context)
			=> (_httpContextAccessor, _context) = (httpContextAccessor, context);
	}
}
