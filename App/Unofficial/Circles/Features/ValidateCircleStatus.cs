using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record ValidateCircleStatusCommand(string Name, string Status) : IRequest<Result<string>>;

class ValidateCircleStatusHandler : BaseHandler<ValidateCircleStatusCommand, string>
{
	public ValidateCircleStatusHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(ValidateCircleStatusCommand command, CancellationToken cancellationToken)
	{
		var circle = await _context.Circles.SingleOrDefaultAsync(c => c.Name == command.Name);

		if (circle is null)
		{
			return _resultFactory.NotFound($"Circle {command.Name} not found.");
		}

		if (circle.Status != UnofficialStatus.Pending)
		{
			return _resultFactory.Conflict($"Circle {command.Name} is already approved. Status = {circle.Status}.");
		}

		circle.Status = Enum.Parse<UnofficialStatus>(command.Status);
		await _context.SaveChangesAsync();

		var message = $"Circle {command.Name} was {command.Status} successfully.";
		return _resultFactory.Ok(message);
	}
}
