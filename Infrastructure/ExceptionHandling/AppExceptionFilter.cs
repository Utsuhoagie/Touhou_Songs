using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Touhou_Songs.Infrastructure.ExceptionHandling
{
	public class AppExceptionFilter : IActionFilter, IOrderedFilter
	{
		public int Order => int.MaxValue - 10;

		public void OnActionExecuting(ActionExecutingContext context) { }

		public void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Exception is AppException exception)
			{
				var valueWithMessage = new
				{
					Message = exception.Value,
				};

				context.Result = new ObjectResult(valueWithMessage)
				{
					StatusCode = (int)exception.StatusCode,
				};

				context.ExceptionHandled = true;
			}
		}
	}
}
