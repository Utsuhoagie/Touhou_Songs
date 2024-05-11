﻿using FluentValidation;
using MediatR;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record CreateCircleCommand : IRequest<Result<CreateCircleCommandResponse>>
{
	public string Name { get; set; }

	public CreateCircleCommand(string name) => (Name) = (name);
};

public record CreateCircleCommandResponse
{
	public string Name { get; set; }

	// "Pending", "Confirmed", "Rejected"
	public string Status { get; set; }

	public CreateCircleCommandResponse(string name, string status) => (Name, Status) = (name, status);
};

public class CreateCircleValidator : AbstractValidator<CreateCircleCommand>
{
	public CreateCircleValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}

class CreateCircleHandler : BaseHandler<CreateCircleCommand, CreateCircleCommandResponse>
{
	private readonly IValidator<CreateCircleCommand> _validator;

	public CreateCircleHandler(AuthUtils authUtils, IValidator<CreateCircleCommand> validator, Touhou_Songs_Context context) : base(authUtils, context)
		=> _validator = validator;

	public override async Task<Result<CreateCircleCommandResponse>> Handle(CreateCircleCommand command, CancellationToken cancellationToken)
	{
		var userWithRole_Result = await _authUtils.GetUserWithRole();

		if (!userWithRole_Result.Success)
		{
			return _resultFactory.FromResult(userWithRole_Result);
		}

		var (user, role) = userWithRole_Result.Value;

		var validationResult = await _validator.ValidateAsync(command);

		if (!validationResult.IsValid)
		{
			var errorMessages = validationResult.Errors.Select(vf => vf.ErrorMessage);
			return _resultFactory.BadRequest(null, errorMessages);
		}

		var circleStatus = role == AuthRole.Admin ?
			UnofficialStatus.Confirmed
			: UnofficialStatus.Pending;

		var circle = new Circle(command.Name, circleStatus)
		{
			ArrangementSongs = new(),
		};

		_context.Circles.Add(circle);
		await _context.SaveChangesAsync();

		var res = new CreateCircleCommandResponse(circle.Name, Enum.GetName(circle.Status)!);
		return _resultFactory.Ok(res);
	}
}
