using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Features.Official.OfficialGames;
using Touhou_Songs.Features.Official.OfficialSongs;

namespace Touhou_Songs.Data
{
	public class Touhou_Songs_Context : DbContext
	{
		public Touhou_Songs_Context(DbContextOptions<Touhou_Songs_Context> options)
			: base(options)
		{
		}

		public DbSet<OfficialGame> OfficialGames { get; set; } = default!;
		public DbSet<OfficialSong> OfficialSongs { get; set; } = default!;
	}
}
