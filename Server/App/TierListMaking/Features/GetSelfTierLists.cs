using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public class GetSelfTierListsQuery : IRequest<Result<GetSelfTierListsResponse>>;

public record GetSelfTierListsResponse
{
	public required List<TierListSimple> TierLists { get; set; }
	public record TierListSimple : BaseAuditedEntityResponse
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public TierListType Type { get; set; }

		public TierListSimple(TierList tl) : base(tl)
			=> (Title, Description, Type) = (tl.Title, tl.Description, tl.Type);
	}
}


class GetSelfTierLists : BaseHandler<GetSelfTierListsQuery, GetSelfTierListsResponse>
{
	public GetSelfTierLists(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<GetSelfTierListsResponse>> Handle(GetSelfTierListsQuery query, CancellationToken cancellationToken)
	{
		var getUserWithRoleResult = await _authUtils.GetUserWithRole();

		if (!getUserWithRoleResult.Success)
		{
			return _resultFactory.Unauthorized();
		}

		var (user, role) = getUserWithRoleResult.Value;

		var tierLists_Res = await _context.UserProfiles
			.Include(up => up.TierLists)
			.Where(up => up.UserId == user.Id)
			.SelectMany(up => up.TierLists)
			.Select(tl => new GetSelfTierListsResponse.TierListSimple(tl))
			.ToListAsync();

		var res = new GetSelfTierListsResponse
		{
			TierLists = tierLists_Res,
		};

		return _resultFactory.Ok(res);
	}
}
