using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public record GetTierListDetailQuery(int Id) : IRequest<Result<GetTierListDetailResponse>>;

public class GetTierListDetailResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public TierListType Type { get; set; }

	public required List<TierListTierResponse> Tiers { get; set; } = new();

	public GetTierListDetailResponse(string title, string? description, TierListType type)
		=> (Title, Description, Type) = (title, description, type);
}

public class TierListTierResponse : BaseEntity
{
	public string Label { get; set; }
	public int Order { get; set; }

	public required List<TierListItemResponse> Items { get; set; }

	public TierListTierResponse(string label, int order) => (Label, Order) = (label, order);
}

public class TierListItemResponse : BaseEntity
{
	public string Label { get; set; }
	public int Order { get; set; }
	public string IconUrl { get; set; }

	public required int SourceId { get; set; }

	public TierListItemResponse(string label, int order, string iconUrl) => (Label, Order, IconUrl) = (label, order, iconUrl);
}

class GetTierListDetailHandler : BaseHandler<GetTierListDetailQuery, GetTierListDetailResponse>
{
	public GetTierListDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context)
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

		var tierListResponse = new GetTierListDetailResponse(dbTierList.Title, dbTierList.Description, dbTierList.Type)
		{
			Id = dbTierList.Id,
			CreatedOn = dbTierList.CreatedOn,
			CreatedByUserName = dbTierList.CreatedByUserName,
			UpdatedOn = dbTierList.UpdatedOn,
			UpdatedByUserName = dbTierList.UpdatedByUserName,
			Tiers = dbTierList.Tiers.Select(tlt => new TierListTierResponse(tlt.Label, tlt.Order)
			{
				Id = tlt.Id,
				Items = tlt.Items.Select(tli => new TierListItemResponse(tli.Label, tli.Order, tli.IconUrl)
				{
					Id = tli.Id,
					SourceId = tli.SourceId,
				}).ToList(),
			}).ToList(),
		};

		return _resultFactory.Ok(tierListResponse);
	}
}