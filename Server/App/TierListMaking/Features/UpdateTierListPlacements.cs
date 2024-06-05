using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public record UpdateTierListPlacementsCommand(int TierListId, UpdateTierListPlacementsPayload Payload) : IRequest<Result<UpdateTierListPlacementsResponse>>;

public class UpdateTierListPlacementsPayload
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

			public string Label { get; set; } = string.Empty;
			public int Order { get; set; }
			public string IconUrl { get; set; } = string.Empty;
		}
	}
}

public class UpdateTierListPlacementsResponse : BaseAuditedEntityResponse;

class UpdateTierListPlacementsHandler : BaseHandler<UpdateTierListPlacementsCommand, UpdateTierListPlacementsResponse>
{
	public UpdateTierListPlacementsHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context)
	{
	}

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

		var dbSourceIdsOfTierListItems = request.Payload.Tiers
			.SelectMany(tlt => tlt.Items)
			.Select(tli => tli.SourceId);

		IEnumerable<BaseAuditedEntity> dbSourcesOfTierListItems = dbTierList.Type switch
		{
			TierListType.ArrangementSongs => await _context.ArrangementSongs
				.Where(@as => dbSourceIdsOfTierListItems.Contains(@as.Id))
				.ToListAsync(),
			TierListType.OfficialGames => await _context.OfficialGames
				.Where(og => dbSourceIdsOfTierListItems.Contains(og.Id))
				.ToListAsync(),
			_ => Enumerable.Empty<BaseAuditedEntity>(),
		};

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

					return new TierListItem(tli.Label, tli.Order, tli.IconUrl)
					{
						SourceId = dbSourceOfTierListItem.Id,
						Source = dbSourceOfTierListItem,
					};
				}).ToList(),
			})
			.ToList();

		dbTierList.Tiers = newTiers;

		var dbTierListEntry = _context.Entry(dbTierList);
		dbTierListEntry.State = EntityState.Modified;

		await _context.SaveChangesAsync();

		var response = new UpdateTierListPlacementsResponse
		{
			Id = dbTierList.Id,
			CreatedOn = dbTierList.CreatedOn,
			CreatedByUserName = dbTierList.CreatedByUserName,
			UpdatedOn = dbTierList.UpdatedOn,
			UpdatedByUserName = dbTierList.UpdatedByUserName,
		};

		return _resultFactory.Ok(response);
	}
}