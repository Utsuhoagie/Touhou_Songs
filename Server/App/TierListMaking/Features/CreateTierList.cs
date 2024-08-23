using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public record CreateTierListCommand(string Title, string? Description, TierListType Type) : IRequest<Result<CreateTierListResponse>>;

public record CreateTierListResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public TierListType Type { get; set; }

	public CreateTierListResponse(TierList tierList) : base(tierList)
		=> (Title, Description, Type) = (tierList.Title, tierList.Description, tierList.Type);
};

class CreateTierListHandler : BaseHandler<CreateTierListCommand, CreateTierListResponse>
{
	public CreateTierListHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<CreateTierListResponse>> Handle(CreateTierListCommand command, CancellationToken cancellationToken)
	{
		var dbCurrentUserWithRoleResult = await _authUtils.GetUserWithRole();
		var dbCurrentProfile = dbCurrentUserWithRoleResult.Value.User.Profile;

		if (!dbCurrentUserWithRoleResult.Success || dbCurrentProfile is null)
		{
			return _resultFactory.Unauthorized("Must have profile to create a Tier list");
		}

		var dbTierListSameTitle = await _context.TierLists
			.SingleOrDefaultAsync(tl => tl.Title == command.Title);

		if (dbTierListSameTitle is not null)
		{
			return _resultFactory.Conflict(GenericI18n.Conflict.ToLanguage(Lang.EN, $"Tier list with title [{command.Title}] already exists"));
		}

		var (Title, Description, Type) = command;
		var tierList = new TierList(Title, Description, Type);

		dbCurrentProfile.AddTierList(tierList);
		var createdTierList = _context.TierLists.Add(tierList).Entity;

		await _context.SaveChangesAsync();

		var res = new CreateTierListResponse(createdTierList);

		return _resultFactory.Ok(res);
	}
}