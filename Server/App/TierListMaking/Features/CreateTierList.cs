using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
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
	public CreateTierListHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<CreateTierListResponse>> Handle(CreateTierListCommand request, CancellationToken cancellationToken)
	{
		var dbCurrentUserWithRole_Res = await _authUtils.GetUserWithRole();
		var dbCurrentProfile = dbCurrentUserWithRole_Res.Value.User.Profile;

		if (!dbCurrentUserWithRole_Res.Success || dbCurrentProfile is null)
		{
			return _resultFactory.Unauthorized("Must have profile to create a Tier list");
		}

		var dbTierListSameTitle = await _context.TierLists.SingleOrDefaultAsync(tl => tl.Title == request.Title);

		if (dbTierListSameTitle is not null)
		{
			return _resultFactory.Conflict($"Tier list with title [{request.Title}] already exists");
		}

		var (Title, Description, Type) = request;
		var tierList = new TierList(Title, Description, Type);

		dbCurrentProfile.AddTierList(tierList);
		var createdTierList = _context.TierLists.Add(tierList).Entity;

		await _context.SaveChangesAsync();

		var res = new CreateTierListResponse(createdTierList);

		return _resultFactory.Ok(res);
	}
}