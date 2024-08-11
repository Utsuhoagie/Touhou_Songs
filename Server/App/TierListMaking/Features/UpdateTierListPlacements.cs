using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public record UpdateTierListPlacementsCommand(int TierListId, UpdateTierListPlacementsPayload Payload) : IRequest<Result<UpdateTierListPlacementsResponse>>;

public record UpdateTierListPlacementsPayload
{
	public List<NewTierListTier> Tiers { get; set; } = new();
	public class NewTierListTier
	{
		public string Label { get; set; } = string.Empty;
		public int Order { get; set; }

		public List<NewTierListItem> Items { get; set; } = new();
		public class NewTierListItem
		{
			public int SourceId { get; set; }

			public int Order { get; set; }
			public string IconUrl { get; set; } = string.Empty;
		}
	}
}

public record UpdateTierListPlacementsResponse : BaseAuditedEntityResponse
{
	public UpdateTierListPlacementsResponse(BaseAuditedEntity entity) : base(entity) { }
}

class UpdateTierListPlacementsHandler : BaseHandler<UpdateTierListPlacementsCommand, UpdateTierListPlacementsResponse>
{
	private readonly TierListRepository _tierListRepository;
	public UpdateTierListPlacementsHandler(AuthUtils authUtils, AppDbContext context, TierListRepository tierListRepository) : base(authUtils, context)
		=> _tierListRepository = tierListRepository;

	public override async Task<Result<UpdateTierListPlacementsResponse>> Handle(UpdateTierListPlacementsCommand request, CancellationToken cancellationToken)
	{
		var dbTierList = await _context.TierLists
			.Include(tl => tl.Tiers)
				.ThenInclude(tlt => tlt.Items)
			.SingleOrDefaultAsync(tl => tl.Id == request.TierListId);

		if (dbTierList is null)
		{
			return _resultFactory.NotFound($"Tier list {request.TierListId} not found");
		}

		var validateTierListBelongsToUser = _authUtils.ValidateEntityBelongsToUser(dbTierList);

		if (!validateTierListBelongsToUser.Success)
		{
			return _resultFactory.FromResult(validateTierListBelongsToUser);
		}

		var dbSourceIdsOfTierListItems = request.Payload.Tiers
			.SelectMany(tlt => tlt.Items)
			.Select(tli => tli.SourceId)
			.ToList();

		var dbSourcesOfTierListItems = await _tierListRepository.GetSources(dbTierList.Type, dbSourceIdsOfTierListItems);

		if (dbSourcesOfTierListItems.Count() < dbSourceIdsOfTierListItems.Count())
		{
			return _resultFactory.NotFound($"Some source items were not found");
		}

		var newTiers = request.Payload.Tiers
			.Select(tlt => new TierListTier(tlt.Label, tlt.Order)
			{
				TierList = dbTierList,
				TierListId = dbTierList.Id,
				Items = tlt.Items.Select(tli =>
				{
					var dbSourceOfTierListItem = dbSourcesOfTierListItems
						.Single(i => i.Id == tli.SourceId);

					return new TierListItem(tli.Order, tli.IconUrl)
					{
						//SourceId = dbSourceOfTierListItem.Id,
						//Source = dbSourceOfTierListItem,
					};
				}).ToList(),
			})
			.ToList();

		dbTierList.Tiers = newTiers;

		var dbTierListEntry = _context.Entry(dbTierList);
		dbTierListEntry.State = EntityState.Modified;

		await _context.SaveChangesAsync();

		var updateTierListPlacements_Res = new UpdateTierListPlacementsResponse(dbTierList);

		return _resultFactory.Ok(updateTierListPlacements_Res);
	}
}