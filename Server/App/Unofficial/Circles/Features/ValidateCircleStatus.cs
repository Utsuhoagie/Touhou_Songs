using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record ValidateCircleStatusPayload(UnofficialStatus Status);

public record ValidateCircleStatusCommand(string Name, ValidateCircleStatusPayload Payload) : IRequest<Result<string>>;

class ValidateCircleStatusHandler : BaseHandler<ValidateCircleStatusCommand, string>
{
	public ValidateCircleStatusHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(ValidateCircleStatusCommand command, CancellationToken cancellationToken)
	{
		var dbCircle = await _context.Circles.SingleOrDefaultAsync(c => c.Name == command.Name);

		if (dbCircle is null)
		{
			return _resultFactory.NotFound(GenericI18n.NotFound.ToLanguage(Lang.EN, nameof(Circle), command.Name));
		}

		if (dbCircle.Status != UnofficialStatus.Pending)
		{
			return _resultFactory.Conflict(GenericI18n.Conflict.ToLanguage(Lang.EN, $"Circle {command.Name} is already approved. Status = {dbCircle.Status}."));
		}

		dbCircle.Status = command.Payload.Status;
		await _context.SaveChangesAsync();

		var message = $"Circle {dbCircle.Name} was {dbCircle.Status} successfully.";
		return _resultFactory.Ok(GenericI18n.Success.ToLanguage(Lang.EN, message));
	}
}
