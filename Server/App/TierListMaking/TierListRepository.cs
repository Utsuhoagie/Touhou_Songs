using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseRepository;

namespace Touhou_Songs.App.TierListMaking;

public class TierListRepository : BaseRepository
{
	public TierListRepository(AppDbContext context) : base(context) { }

	public async Task<List<BaseAuditedEntity>> GetSources(TierListType type, List<int>? dbSourceIds = null)
	{
		dbSourceIds ??= Enumerable.Empty<int>().ToList();

		//IQueryable<BaseAuditedEntity> dbSourcesOfTierListItems = type switch
		//{
		//	TierListType.ArrangementSongs => _context.ArrangementSongs
		//		.Where(a => dbSourceIds.Contains(a.Id)),
		//	TierListType.OfficialGames => _context.OfficialGames
		//		.Where(og => dbSourceIds.Contains(og.Id)),
		//};

		IQueryable<BaseAuditedEntity> sourceContext = type switch
		{
			TierListType.ArrangementSongs => _context.ArrangementSongs,
			//TierListType.OfficialGames => _context.OfficialGames,
		};

		var dbSourcesOfTierListItems = await sourceContext
			.Where(s => dbSourceIds.Contains(s.Id))
			.ToListAsync();

		return dbSourcesOfTierListItems;
	}

	public async Task<List<BaseAuditedEntity>> GetRemainingSources(TierList tierList)
	{
		var tierListType = tierList.Type;
		var currentSourceIds = tierList.Tiers
			.SelectMany(tlt => tlt.Items);
		//.Select(tli => tli.SourceId);

		throw new NotImplementedException();

		//IQueryable<BaseAuditedEntity> sourceContext = tierListType switch
		//{
		//	TierListType.ArrangementSongs => _context.ArrangementSongs,
		//	//TierListType.OfficialGames => _context.OfficialGames,
		//};

		//var dbSourcesOfTierListItems = await sourceContext
		//	.Where(s => !currentSourceIds.Contains(s.Id))
		//	.ToListAsync();

		//return dbSourcesOfTierListItems.ToList();
	}
}
