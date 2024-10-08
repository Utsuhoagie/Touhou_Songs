﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
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

	public override async Task<Result<UpdateTierListPlacementsResponse>> Handle(UpdateTierListPlacementsCommand command, CancellationToken cancellationToken)
	{
		var dbTierList = await _context.TierLists
			.Include(tl => tl.Tiers)
				.ThenInclude(tlt => tlt.Items)
			.SingleOrDefaultAsync(tl => tl.Id == command.TierListId);

		if (dbTierList is null)
		{
			return _resultFactory.NotFound(GenericI18n.NotFound.ToLanguage(Lang.EN, nameof(TierList), command.TierListId));
		}

		var validateTierListBelongsToUser = _authUtils.ValidateEntityBelongsToUser(dbTierList);

		if (!validateTierListBelongsToUser.Success)
		{
			return _resultFactory.FromResult(validateTierListBelongsToUser);
		}

		var sourceIdsOfTierListItems = command.Payload.Tiers
			.SelectMany(tlt => tlt.Items)
			.Select(tli => tli.SourceId)
			.ToList();

		if (sourceIdsOfTierListItems.Count > sourceIdsOfTierListItems.Distinct().Count())
		{
			return _resultFactory.BadRequest(GenericI18n.BadRequest.ToLanguage(Lang.EN, $"Duplicate source Ids"));
		}

		var dbSourcesOfTierListItems = await _tierListRepository.GetSources(dbTierList.Type, sourceIdsOfTierListItems);

		if (dbSourcesOfTierListItems.Count < sourceIdsOfTierListItems.Count)
		{
			var foundSourceIds = dbSourcesOfTierListItems.Select(s => s.Id).ToList();
			var notFoundSourceIds = sourceIdsOfTierListItems.Except(foundSourceIds).ToList();
			var message = GenericI18n.NotFound.ToLanguage(Lang.EN,
				$"Source items {dbTierList.Type}", string.Join(", ", notFoundSourceIds));
			return _resultFactory.NotFound(message);
		}

		var newTiers = command.Payload.Tiers
			.Select(tlt => new TierListTier(tlt.Label, tlt.Order)
			{
				TierList = dbTierList,
				TierListId = dbTierList.Id,
				Items = tlt.Items.Select(tli =>
				{
					var dbSourceOfTierListItem = dbSourcesOfTierListItems
						.Single(s => s.Id == tli.SourceId);

					var type = dbTierList.Type;

					return new TierListItem(tli.Order, tli.IconUrl)
					{
						ArrangementSong = type == TierListType.ArrangementSongs ? (ArrangementSong)dbSourceOfTierListItem : null,
						ArrangementSongId = type == TierListType.ArrangementSongs ? dbSourceOfTierListItem.Id : null,

						OfficialGame = type == TierListType.OfficialGames ? (OfficialGame)dbSourceOfTierListItem : null,
						OfficialGameId = type == TierListType.OfficialGames ? dbSourceOfTierListItem.Id : null,
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