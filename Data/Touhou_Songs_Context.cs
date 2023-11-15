using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official._JoinEntities;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.App.Official.OfficialSongs;

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
		public DbSet<Character> Characters { get; set; } = default!;
		public DbSet<CharacterOfficialSong> CharacterOfficialSongs { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Character>()
				.HasOne(c => c.OriginGame)
				.WithMany()
				.HasForeignKey(c => c.OriginGameId)
				.IsRequired();

			modelBuilder.Entity<OfficialSong>()
				.HasMany(os => os.Characters)
				.WithMany(c => c.OfficialSongs)
				.UsingEntity<CharacterOfficialSong>();
		}
	}
}
