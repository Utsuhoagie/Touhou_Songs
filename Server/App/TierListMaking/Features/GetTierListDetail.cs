using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public record GetTierListDetailQuery(int Id) : IRequest<Result<GetTierListDetailResponse>>;

public record GetTierListDetailResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public TierListType Type { get; set; }

	public required List<TierListTierResponse> Tiers { get; set; } = new();

	public GetTierListDetailResponse(TierList tierList) : base(tierList)
		=> (Title, Description, Type) = (tierList.Title, tierList.Description, tierList.Type);
}

public record TierListTierResponse : BaseEntityResponse
{
	public string Label { get; set; }
	public int Order { get; set; }

	public required List<TierListItemResponse> Items { get; set; }

	public TierListTierResponse(TierListTier tierListTier) : base(tierListTier)
		=> (Label, Order) = (tierListTier.Label, tierListTier.Order);
}

public record TierListItemResponse : BaseEntityResponse
{
	public string Label { get; set; }
	public int Order { get; set; }
	public string IconUrl { get; set; }

	public required int SourceId { get; set; }

	public TierListItemResponse(TierListItem tierListItem) : base(tierListItem)
		=> (Label, Order, IconUrl) = (tierListItem.Label, tierListItem.Order, tierListItem.IconUrl);
}

class GetTierListDetailHandler : BaseHandler<GetTierListDetailQuery, GetTierListDetailResponse>
{
	public GetTierListDetailHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context)
	{
	}

	public async override Task<Result<GetTierListDetailResponse>> Handle(GetTierListDetailQuery query, CancellationToken cancellationToken)
	{
		var dbTierList = await _context.TierLists
			.Include(tl => tl.Tiers)
				.ThenInclude(tlt => tlt.Items)
			.SingleOrDefaultAsync(tl => tl.Id == query.Id);

		if (dbTierList is null)
		{
			return _resultFactory.NotFound($"Tier list {query.Id} not found");
		}

		var tierList_Res = new GetTierListDetailResponse(dbTierList)
		{
			Tiers = dbTierList.Tiers.Select(tlt => new TierListTierResponse(tlt)
			{
				Items = tlt.Items.Select(tli => new TierListItemResponse(tli)
				{
					SourceId = tli.SourceId,
				}).ToList(),
			}).ToList(),
		};

		return _resultFactory.Ok(tierList_Res);
	}
}