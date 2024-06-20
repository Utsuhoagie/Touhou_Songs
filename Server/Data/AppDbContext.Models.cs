using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App._JoinEntities;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.App.TierListMaking;
using Touhou_Songs.App.Unofficial.Circles;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.App.UserProfile;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.Data;

public partial class AppDbContext : IdentityDbContext<AppUser>
{
	#region ---- Official ----
	public DbSet<OfficialGame> OfficialGames { get; set; } = default!;
	public DbSet<OfficialSong> OfficialSongs { get; set; } = default!;
	public DbSet<Character> Characters { get; set; } = default!;
	public DbSet<CharacterOfficialSong> CharacterOfficialSongs { get; set; } = default!;
	#endregion

	#region ---- Unofficial ----
	public DbSet<Circle> Circles { get; set; } = default!;
	public DbSet<ArrangementSong> ArrangementSongs { get; set; } = default!;
	#endregion

	#region ---- Profile ----
	public DbSet<UserProfile> UserProfiles { get; set; } = default!;
	public DbSet<TierList> TierLists { get; set; } = default!;
	public DbSet<TierListTier> TierListTiers { get; set; } = default!;
	public DbSet<TierListItem> TierListItems { get; set; } = default!;
	#endregion
}