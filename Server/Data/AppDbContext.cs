﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App._JoinEntities;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.App.TierListMaking;
using Touhou_Songs.App.Unofficial.Circles;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.App.UserProfile;
using Touhou_Songs.Infrastructure.Auth.Models;

namespace Touhou_Songs.Data;

public partial class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	void OnModelCreating_Auth(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<AppUser>()
			.HasMany(u => u.UserRoles)
			.WithOne(ur => ur.User)
			.HasForeignKey(ur => ur.UserId)
			.IsRequired();

		modelBuilder.Entity<AppRole>()
			.HasMany(r => r.UserRoles)
			.WithOne(ur => ur.Role)
			.HasForeignKey(ur => ur.RoleId)
			.IsRequired();

		modelBuilder.Entity<AppRole>()
			.HasData(
				new AppRole
				{
					Id = "8c49af1b-5822-4cbd-bd7c-967dc49a54d4",
					Name = "Admin",
					NormalizedName = "ADMIN",
					ConcurrencyStamp = null,
				},
				new AppRole
				{
					Id = "48807e45-822f-4da9-bbf3-5e71beff314a",
					Name = "User",
					NormalizedName = "USER",
					ConcurrencyStamp = null,
				}
			);
	}

	void OnModelCreating_App(ModelBuilder modelBuilder)
	{
		//modelBuilder.Entity<BaseEntity>()
		//	.UseTpcMappingStrategy();


		modelBuilder.Entity<Character>()
			.HasOne(c => c.OriginGame)
			.WithMany()
			.HasForeignKey(c => c.OriginGameId)
			.IsRequired();


		modelBuilder.Entity<OfficialSong>()
			.HasMany(os => os.Characters)
			.WithMany(c => c.OfficialSongs)
			.UsingEntity<CharacterOfficialSong>(b =>
			{
				b.ToTable("_CharacterOfficialSong");
				b.HasOne(cos => cos.OfficialSong)
					.WithMany(os => os.CharacterOfficialSongs)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);
			});


		modelBuilder.Entity<Circle>()
			.HasMany(c => c.ArrangementSongs)
			.WithOne(a => a.Circle)
			.IsRequired();

		modelBuilder.Entity<Circle>()
			.Property(c => c.Status)
			.HasConversion<string>();


		modelBuilder.Entity<ArrangementSong>()
			.HasMany(a => a.OfficialSongs)
			.WithMany(os => os.ArrangementSongs)
			.UsingEntity<OfficialSongArrangementSong>();

		modelBuilder.Entity<ArrangementSong>()
			.Property(a => a.Status)
			.HasConversion<string>();


		modelBuilder.Entity<AppUser>()
			.HasOne(u => u.Profile)
			.WithOne(up => up.User)
			.HasForeignKey<UserProfile>(up => up.UserId)
			.IsRequired();


		modelBuilder.Entity<TierList>()
			.Property(tl => tl.Type)
			.HasConversion(
				val => Enum.GetName(val),
				dbVal => Enum.Parse<TierListType>(dbVal!))
			.HasDefaultValue(TierListType.ArrangementSongs);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		OnModelCreating_Auth(modelBuilder);

		OnModelCreating_App(modelBuilder);
	}
}