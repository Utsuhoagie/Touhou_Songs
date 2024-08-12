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

		IQueryable<BaseAuditedEntity> sourceContext = type switch
		{
			TierListType.OfficialGames => _context.OfficialGames,
			TierListType.ArrangementSongs => _context.ArrangementSongs,
			_ => null!,
		};

		var dbSourcesOfTierListItems = await sourceContext
			.Where(s => dbSourceIds.Contains(s.Id))
			.ToListAsync();

		return dbSourcesOfTierListItems;
	}

	public async Task<List<BaseAuditedEntity>> GetRemainingSources(TierList tierList)
	{
		var type = tierList.Type;

		var currentSourceIds = tierList.Tiers
			.SelectMany(tlt => tlt.Items)
			.Select(tli => type switch
			{
				TierListType.OfficialGames => tli.OfficialGameId!.Value,
				TierListType.ArrangementSongs => tli.ArrangementSongId!.Value,
				_ => default,
			})
			.ToList();

		IQueryable<BaseAuditedEntity> sourceContext = type switch
		{
			TierListType.OfficialGames => _context.OfficialGames,
			TierListType.ArrangementSongs => _context.ArrangementSongs,
			_ => default!,
		};

		var dbSourcesOfTierListItems = await sourceContext
			.Where(s => !currentSourceIds.Contains(s.Id))
			.ToListAsync();

		return dbSourcesOfTierListItems;
	}
}
