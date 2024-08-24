using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Paging;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public record GetSelfTierListsQuery : PagingParams, IRequest<Result<Paged<TierListResponse>>>;

public record TierListResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public TierListType Type { get; set; }

	public TierListResponse(TierList tl) : base(tl)
		=> (Title, Description, Type) = (tl.Title, tl.Description, tl.Type);
}

class GetSelfTierLists : BaseHandler<GetSelfTierListsQuery, Paged<TierListResponse>>
{
	public GetSelfTierLists(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<Paged<TierListResponse>>> Handle(GetSelfTierListsQuery query, CancellationToken cancellationToken)
	{
		var getUserWithRoleResult = await _authUtils.GetUserWithRole();

		if (!getUserWithRoleResult.Success)
		{
			return _resultFactory.Unauthorized();
		}

		var (user, role) = getUserWithRoleResult.Value;

		var getSelfTierListsQuery = _context.UserProfiles
			.Include(up => up.TierLists)
			.Where(up => up.UserId == user.Id)
			.OrderByDescending(up => up.CreatedOn);

		var tierLists_Res = await getSelfTierListsQuery
			.Skip((query.Page - 1) * query.PageSize)
			.Take(query.PageSize)
			.SelectMany(up => up.TierLists)
			.Select(tl => new TierListResponse(tl))
			.ToListAsync();

		var totalTierLists = await getSelfTierListsQuery.CountAsync();

		var pagedTierLists_Res = new Paged<TierListResponse>(query)
		{
			TotalItemsCount = totalTierLists,
			Items = tierLists_Res,
		};

		return _resultFactory.Ok(pagedTierLists_Res);
	}
}
