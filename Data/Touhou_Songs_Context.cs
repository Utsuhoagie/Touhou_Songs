﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official._JoinEntities;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.App.Unofficial.Circles;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.Data
{
	public class Touhou_Songs_Context : IdentityDbContext<AppUser>
	{
		public Touhou_Songs_Context(DbContextOptions<Touhou_Songs_Context> options)
			: base(options)
		{
		}

		public DbSet<OfficialGame> OfficialGames { get; set; } = default!;
		public DbSet<OfficialSong> OfficialSongs { get; set; } = default!;
		public DbSet<Character> Characters { get; set; } = default!;
		public DbSet<CharacterOfficialSong> CharacterOfficialSongs { get; set; } = default!;

		public DbSet<Circle> Circles { get; set; } = default!;
		public DbSet<ArrangementSong> ArrangementSongs { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Auth
			modelBuilder.Entity<IdentityRole>()
				.HasData(
					new IdentityRole
					{
						Id = "8c49af1b-5822-4cbd-bd7c-967dc49a54d4",
						Name = "Admin",
						NormalizedName = "ADMIN",
						ConcurrencyStamp = null,
					},
					new IdentityRole
					{
						Id = "48807e45-822f-4da9-bbf3-5e71beff314a",
						Name = "User",
						NormalizedName = "USER",
						ConcurrencyStamp = null,
					}
				);

			// App
			modelBuilder.Entity<Character>()
				.HasOne(c => c.OriginGame)
				.WithMany()
				.HasForeignKey(c => c.OriginGameId)
				.IsRequired();

			modelBuilder.Entity<OfficialSong>()
				.HasMany(os => os.Characters)
				.WithMany(c => c.OfficialSongs)
				.UsingEntity<CharacterOfficialSong>();

			modelBuilder.Entity<Circle>()
				.HasMany(c => c.ArrangementSongs)
				.WithOne(a => a.Circle)
				.IsRequired();

			modelBuilder.Entity<ArrangementSong>()
				.HasMany(a => a.OfficialSongs)
				.WithMany(os => os.ArrangementSongs)
				.UsingEntity<OfficialSongArrangementSong>();
		}
	}
}
