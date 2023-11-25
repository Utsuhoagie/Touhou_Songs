using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Unofficial.Circles.Features
{
	public record ValidateCircleStatusCommand(string Name, string Status) : IRequest<string>;

	class ValidateCircleStatusCommandHandler : BaseHandler<ValidateCircleStatusCommand, string>
	{
		public ValidateCircleStatusCommandHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

		public override async Task<string> Handle(ValidateCircleStatusCommand command, CancellationToken cancellationToken)
		{
			var circle = await _context.Circles.SingleOrDefaultAsync(c => c.Name == command.Name);

			if (circle is null)
			{
				throw new AppException(HttpStatusCode.NotFound, $"Circle {command.Name} not found.");
			}

			if (circle.Status != UnofficialStatus.Pending)
			{
				throw new AppException(HttpStatusCode.Conflict, $"Circle {command.Name} is already approved. Status = {circle.Status.ToString()}.");
			}

			circle.Status = Enum.Parse<UnofficialStatus>(command.Status);
			await _context.SaveChangesAsync();

			return $"Circle {command.Name} was {command.Status} successfully.";
		}
	}
}
