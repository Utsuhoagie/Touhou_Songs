using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.TierListMaking.Features;

public record GetTierListDetailQuery(int Id) : IRequest<Result<GetTierListDetailResponse>>;

public record GetTierListDetailResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public TierListType Type { get; set; }

	public required List<TierListTierResponse> Tiers { get; set; } = new();
	public record TierListTierResponse : BaseEntityResponse
	{
		public string Label { get; set; }
		public int Order { get; set; }

		public required List<TierListItemResponse> Items { get; set; }
		public record TierListItemResponse : BaseEntityResponse
		{
			public int Order { get; set; }
			public string IconUrl { get; set; }

			public required SourceResponse Source { get; set; }

			public TierListItemResponse(TierListItem tierListItem) : base(tierListItem)
				=> (Order, IconUrl) = (tierListItem.Order, tierListItem.IconUrl);
		}

		public TierListTierResponse(TierListTier tierListTier) : base(tierListTier)
			=> (Label, Order) = (tierListTier.Label, tierListTier.Order);
	}


	public required List<SourceResponse> RemainingSourceItems { get; set; }

	public record SourceResponse : BaseEntityResponse
	{
		public required string IdForRoute { get; set; }
		public string Label { get; set; }
		public string ImageUrl { get; set; }

		public SourceResponse(BaseEntity entity) : base(entity)
		{
			Label = entity.GetLabel();
			ImageUrl = entity.GetImageUrl()!;
		}
	}

	public GetTierListDetailResponse(TierList tierList) : base(tierList)
		=> (Title, Description, Type) = (tierList.Title, tierList.Description, tierList.Type);
}

class GetTierListDetailHandler : BaseHandler<GetTierListDetailQuery, GetTierListDetailResponse>
{
	private readonly TierListRepository _tierListRepository;
	public GetTierListDetailHandler(AuthUtils authUtils, AppDbContext context, TierListRepository tierListRepository) : base(authUtils, context)
		=> _tierListRepository = tierListRepository;

	public async override Task<Result<GetTierListDetailResponse>> Handle(GetTierListDetailQuery query, CancellationToken cancellationToken)
	{
		var dbTierList = await _context.TierLists
			.Include(tl => tl.Tiers)
				.ThenInclude(tlt => tlt.Items)
				.ThenInclude(tli => tli.OfficialGame)
			.Include(tl => tl.Tiers)
				.ThenInclude(tlt => tlt.Items)
				.ThenInclude(tli => tli.ArrangementSong)
			.SingleOrDefaultAsync(tl => tl.Id == query.Id, cancellationToken);

		if (dbTierList is null)
		{
			return _resultFactory.NotFound(GenericI18n.NotFound.ToLanguage(Lang.EN, nameof(TierList), query.Id));
		}

		var dbRemainingSourceItems = await _tierListRepository.GetRemainingSources(dbTierList);

		var tierList_Res = new GetTierListDetailResponse(dbTierList)
		{
			Tiers = dbTierList.Tiers
				.Select(tlt => new GetTierListDetailResponse.TierListTierResponse(tlt)
				{
					Items = tlt.Items
						.Select(tli =>
						{
							BaseAuditedEntity source = tli.TierListTier.TierList.Type switch
							{
								TierListType.OfficialGames => tli.OfficialGame!,
								TierListType.ArrangementSongs => tli.ArrangementSong!,
								_ => default!,
							};

							return new GetTierListDetailResponse.TierListTierResponse.TierListItemResponse(tli)
							{
								Source = new GetTierListDetailResponse.SourceResponse(source)
								{
									IdForRoute = source.GetIdForRoute(),
								},
							};
						})
						.OrderBy(tli => tli.Order)
						.ToList(),
				})
				.OrderBy(tlt => tlt.Order)
				.ToList(),
			RemainingSourceItems = dbRemainingSourceItems
				.Select(s => new GetTierListDetailResponse.SourceResponse(s)
				{
					IdForRoute = s.GetIdForRoute(),
				})
				.ToList(),
		};

		return _resultFactory.Ok(tierList_Res);
	}
}