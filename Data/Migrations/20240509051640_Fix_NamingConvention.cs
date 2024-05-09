using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations;

/// <inheritdoc />
public partial class Fix_NamingConvention : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		//migrationBuilder.DropForeignKey(
		//    name: "FK_ArrangementSongs_Circles_CircleId",
		//    table: "ArrangementSongs");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
		//    table: "AspNetRoleClaims");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
		//    table: "AspNetUserClaims");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
		//    table: "AspNetUserLogins");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
		//    table: "AspNetUserRoles");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
		//    table: "AspNetUserRoles");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
		//    table: "AspNetUserTokens");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_CharacterOfficialSongs_Characters_CharacterId",
		//    table: "CharacterOfficialSongs");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_CharacterOfficialSongs_OfficialSongs_OfficialSongId",
		//    table: "CharacterOfficialSongs");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_Characters_OfficialGames_OriginGameId",
		//    table: "Characters");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_OfficialSongArrangementSong_ArrangementSongs_ArrangementSon~",
		//    table: "OfficialSongArrangementSong");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_OfficialSongArrangementSong_OfficialSongs_OfficialSongId",
		//    table: "OfficialSongArrangementSong");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_OfficialSongs_OfficialGames_GameId",
		//    table: "OfficialSongs");

		//migrationBuilder.DropForeignKey(
		//    name: "FK_UserProfiles_AspNetUsers_UserId",
		//    table: "UserProfiles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_Circles",
		//    table: "Circles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_Characters",
		//    table: "Characters");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_AspNetUserTokens",
		//    table: "AspNetUserTokens");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_AspNetUsers",
		//    table: "AspNetUsers");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_AspNetUserRoles",
		//    table: "AspNetUserRoles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_AspNetUserLogins",
		//    table: "AspNetUserLogins");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_AspNetUserClaims",
		//    table: "AspNetUserClaims");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_AspNetRoles",
		//    table: "AspNetRoles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_AspNetRoleClaims",
		//    table: "AspNetRoleClaims");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_UserProfiles",
		//    table: "UserProfiles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_OfficialSongs",
		//    table: "OfficialSongs");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_OfficialSongArrangementSong",
		//    table: "OfficialSongArrangementSong");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_OfficialGames",
		//    table: "OfficialGames");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_CharacterOfficialSongs",
		//    table: "CharacterOfficialSongs");

		//migrationBuilder.DropPrimaryKey(
		//    name: "PK_ArrangementSongs",
		//    table: "ArrangementSongs");

		migrationBuilder.RenameTable(
			name: "Circles",
			newName: "circles");

		migrationBuilder.RenameTable(
			name: "Characters",
			newName: "characters");

		migrationBuilder.RenameTable(
			name: "UserProfiles",
			newName: "user_profiles");

		migrationBuilder.RenameTable(
			name: "OfficialSongs",
			newName: "official_songs");

		migrationBuilder.RenameTable(
			name: "OfficialSongArrangementSong",
			newName: "official_song_arrangement_song");

		migrationBuilder.RenameTable(
			name: "OfficialGames",
			newName: "official_games");

		migrationBuilder.RenameTable(
			name: "CharacterOfficialSongs",
			newName: "character_official_songs");

		migrationBuilder.RenameTable(
			name: "ArrangementSongs",
			newName: "arrangement_songs");

		migrationBuilder.RenameColumn(
			name: "Status",
			table: "circles",
			newName: "status");

		migrationBuilder.RenameColumn(
			name: "Name",
			table: "circles",
			newName: "name");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "circles",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "Name",
			table: "characters",
			newName: "name");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "characters",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "OriginGameId",
			table: "characters",
			newName: "origin_game_id");

		migrationBuilder.RenameColumn(
			name: "ImageUrl",
			table: "characters",
			newName: "image_url");

		migrationBuilder.RenameIndex(
			name: "IX_Characters_OriginGameId",
			table: "characters",
			newName: "ix_characters_origin_game_id");

		migrationBuilder.RenameColumn(
			name: "Value",
			table: "AspNetUserTokens",
			newName: "value");

		migrationBuilder.RenameColumn(
			name: "Name",
			table: "AspNetUserTokens",
			newName: "name");

		migrationBuilder.RenameColumn(
			name: "LoginProvider",
			table: "AspNetUserTokens",
			newName: "login_provider");

		migrationBuilder.RenameColumn(
			name: "UserId",
			table: "AspNetUserTokens",
			newName: "user_id");

		migrationBuilder.RenameColumn(
			name: "Email",
			table: "AspNetUsers",
			newName: "email");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "AspNetUsers",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "UserName",
			table: "AspNetUsers",
			newName: "user_name");

		migrationBuilder.RenameColumn(
			name: "TwoFactorEnabled",
			table: "AspNetUsers",
			newName: "two_factor_enabled");

		migrationBuilder.RenameColumn(
			name: "SecurityStamp",
			table: "AspNetUsers",
			newName: "security_stamp");

		migrationBuilder.RenameColumn(
			name: "ProfileId",
			table: "AspNetUsers",
			newName: "profile_id");

		migrationBuilder.RenameColumn(
			name: "PhoneNumberConfirmed",
			table: "AspNetUsers",
			newName: "phone_number_confirmed");

		migrationBuilder.RenameColumn(
			name: "PhoneNumber",
			table: "AspNetUsers",
			newName: "phone_number");

		migrationBuilder.RenameColumn(
			name: "PasswordHash",
			table: "AspNetUsers",
			newName: "password_hash");

		migrationBuilder.RenameColumn(
			name: "NormalizedUserName",
			table: "AspNetUsers",
			newName: "normalized_user_name");

		migrationBuilder.RenameColumn(
			name: "NormalizedEmail",
			table: "AspNetUsers",
			newName: "normalized_email");

		migrationBuilder.RenameColumn(
			name: "LockoutEnd",
			table: "AspNetUsers",
			newName: "lockout_end");

		migrationBuilder.RenameColumn(
			name: "LockoutEnabled",
			table: "AspNetUsers",
			newName: "lockout_enabled");

		migrationBuilder.RenameColumn(
			name: "EmailConfirmed",
			table: "AspNetUsers",
			newName: "email_confirmed");

		migrationBuilder.RenameColumn(
			name: "ConcurrencyStamp",
			table: "AspNetUsers",
			newName: "concurrency_stamp");

		migrationBuilder.RenameColumn(
			name: "AccessFailedCount",
			table: "AspNetUsers",
			newName: "access_failed_count");

		migrationBuilder.RenameColumn(
			name: "RoleId",
			table: "AspNetUserRoles",
			newName: "role_id");

		migrationBuilder.RenameColumn(
			name: "UserId",
			table: "AspNetUserRoles",
			newName: "user_id");

		migrationBuilder.RenameIndex(
			name: "IX_AspNetUserRoles_RoleId",
			table: "AspNetUserRoles",
			newName: "ix_asp_net_user_roles_role_id");

		migrationBuilder.RenameColumn(
			name: "UserId",
			table: "AspNetUserLogins",
			newName: "user_id");

		migrationBuilder.RenameColumn(
			name: "ProviderDisplayName",
			table: "AspNetUserLogins",
			newName: "provider_display_name");

		migrationBuilder.RenameColumn(
			name: "ProviderKey",
			table: "AspNetUserLogins",
			newName: "provider_key");

		migrationBuilder.RenameColumn(
			name: "LoginProvider",
			table: "AspNetUserLogins",
			newName: "login_provider");

		migrationBuilder.RenameIndex(
			name: "IX_AspNetUserLogins_UserId",
			table: "AspNetUserLogins",
			newName: "ix_asp_net_user_logins_user_id");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "AspNetUserClaims",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "UserId",
			table: "AspNetUserClaims",
			newName: "user_id");

		migrationBuilder.RenameColumn(
			name: "ClaimValue",
			table: "AspNetUserClaims",
			newName: "claim_value");

		migrationBuilder.RenameColumn(
			name: "ClaimType",
			table: "AspNetUserClaims",
			newName: "claim_type");

		migrationBuilder.RenameIndex(
			name: "IX_AspNetUserClaims_UserId",
			table: "AspNetUserClaims",
			newName: "ix_asp_net_user_claims_user_id");

		migrationBuilder.RenameColumn(
			name: "Name",
			table: "AspNetRoles",
			newName: "name");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "AspNetRoles",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "NormalizedName",
			table: "AspNetRoles",
			newName: "normalized_name");

		migrationBuilder.RenameColumn(
			name: "ConcurrencyStamp",
			table: "AspNetRoles",
			newName: "concurrency_stamp");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "AspNetRoleClaims",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "RoleId",
			table: "AspNetRoleClaims",
			newName: "role_id");

		migrationBuilder.RenameColumn(
			name: "ClaimValue",
			table: "AspNetRoleClaims",
			newName: "claim_value");

		migrationBuilder.RenameColumn(
			name: "ClaimType",
			table: "AspNetRoleClaims",
			newName: "claim_type");

		migrationBuilder.RenameIndex(
			name: "IX_AspNetRoleClaims_RoleId",
			table: "AspNetRoleClaims",
			newName: "ix_asp_net_role_claims_role_id");

		migrationBuilder.RenameColumn(
			name: "Bio",
			table: "user_profiles",
			newName: "bio");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "user_profiles",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "UserId",
			table: "user_profiles",
			newName: "user_id");

		migrationBuilder.RenameColumn(
			name: "AvatarUrl",
			table: "user_profiles",
			newName: "avatar_url");

		migrationBuilder.RenameIndex(
			name: "IX_UserProfiles_UserId",
			table: "user_profiles",
			newName: "ix_user_profiles_user_id");

		migrationBuilder.RenameColumn(
			name: "Title",
			table: "official_songs",
			newName: "title");

		migrationBuilder.RenameColumn(
			name: "Context",
			table: "official_songs",
			newName: "context");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "official_songs",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "GameId",
			table: "official_songs",
			newName: "game_id");

		migrationBuilder.RenameIndex(
			name: "IX_OfficialSongs_GameId",
			table: "official_songs",
			newName: "ix_official_songs_game_id");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "official_song_arrangement_song",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "OfficialSongId",
			table: "official_song_arrangement_song",
			newName: "official_song_id");

		migrationBuilder.RenameColumn(
			name: "ArrangementSongId",
			table: "official_song_arrangement_song",
			newName: "arrangement_song_id");

		migrationBuilder.RenameIndex(
			name: "IX_OfficialSongArrangementSong_OfficialSongId",
			table: "official_song_arrangement_song",
			newName: "ix_official_song_arrangement_song_official_song_id");

		migrationBuilder.RenameIndex(
			name: "IX_OfficialSongArrangementSong_ArrangementSongId",
			table: "official_song_arrangement_song",
			newName: "ix_official_song_arrangement_song_arrangement_song_id");

		migrationBuilder.RenameColumn(
			name: "Title",
			table: "official_games",
			newName: "title");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "official_games",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "ReleaseDate",
			table: "official_games",
			newName: "release_date");

		migrationBuilder.RenameColumn(
			name: "NumberCode",
			table: "official_games",
			newName: "number_code");

		migrationBuilder.RenameColumn(
			name: "ImageUrl",
			table: "official_games",
			newName: "image_url");

		migrationBuilder.RenameColumn(
			name: "GameCode",
			table: "official_games",
			newName: "game_code");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "character_official_songs",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "OfficialSongId",
			table: "character_official_songs",
			newName: "official_song_id");

		migrationBuilder.RenameColumn(
			name: "CharacterId",
			table: "character_official_songs",
			newName: "character_id");

		migrationBuilder.RenameIndex(
			name: "IX_CharacterOfficialSongs_OfficialSongId",
			table: "character_official_songs",
			newName: "ix_character_official_songs_official_song_id");

		migrationBuilder.RenameIndex(
			name: "IX_CharacterOfficialSongs_CharacterId",
			table: "character_official_songs",
			newName: "ix_character_official_songs_character_id");

		migrationBuilder.RenameColumn(
			name: "Url",
			table: "arrangement_songs",
			newName: "url");

		migrationBuilder.RenameColumn(
			name: "Title",
			table: "arrangement_songs",
			newName: "title");

		migrationBuilder.RenameColumn(
			name: "Status",
			table: "arrangement_songs",
			newName: "status");

		migrationBuilder.RenameColumn(
			name: "Id",
			table: "arrangement_songs",
			newName: "id");

		migrationBuilder.RenameColumn(
			name: "TitleRomaji",
			table: "arrangement_songs",
			newName: "title_romaji");

		migrationBuilder.RenameColumn(
			name: "TitleJapanese",
			table: "arrangement_songs",
			newName: "title_japanese");

		migrationBuilder.RenameColumn(
			name: "CircleId",
			table: "arrangement_songs",
			newName: "circle_id");

		migrationBuilder.RenameIndex(
			name: "IX_ArrangementSongs_CircleId",
			table: "arrangement_songs",
			newName: "ix_arrangement_songs_circle_id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_circles",
		//    table: "circles",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_characters",
		//    table: "characters",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_asp_net_user_tokens",
		//    table: "AspNetUserTokens",
		//    columns: new[] { "user_id", "login_provider", "name" });

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_asp_net_users",
		//    table: "AspNetUsers",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_asp_net_user_roles",
		//    table: "AspNetUserRoles",
		//    columns: new[] { "user_id", "role_id" });

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_asp_net_user_logins",
		//    table: "AspNetUserLogins",
		//    columns: new[] { "login_provider", "provider_key" });

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_asp_net_user_claims",
		//    table: "AspNetUserClaims",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_asp_net_roles",
		//    table: "AspNetRoles",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_asp_net_role_claims",
		//    table: "AspNetRoleClaims",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_user_profiles",
		//    table: "user_profiles",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_official_songs",
		//    table: "official_songs",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_official_song_arrangement_song",
		//    table: "official_song_arrangement_song",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_official_games",
		//    table: "official_games",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_character_official_songs",
		//    table: "character_official_songs",
		//    column: "id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "pk_arrangement_songs",
		//    table: "arrangement_songs",
		//    column: "id");

		//migrationBuilder.AddForeignKey(
		//    name: "fk_arrangement_songs_circles_circle_id",
		//    table: "arrangement_songs",
		//    column: "circle_id",
		//    principalTable: "circles",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_asp_net_role_claims_asp_net_roles_role_id",
		//    table: "AspNetRoleClaims",
		//    column: "role_id",
		//    principalTable: "AspNetRoles",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_asp_net_user_claims_asp_net_users_user_id",
		//    table: "AspNetUserClaims",
		//    column: "user_id",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_asp_net_user_logins_asp_net_users_user_id",
		//    table: "AspNetUserLogins",
		//    column: "user_id",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_asp_net_user_roles_asp_net_roles_role_id",
		//    table: "AspNetUserRoles",
		//    column: "role_id",
		//    principalTable: "AspNetRoles",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_asp_net_user_roles_asp_net_users_user_id",
		//    table: "AspNetUserRoles",
		//    column: "user_id",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_asp_net_user_tokens_asp_net_users_user_id",
		//    table: "AspNetUserTokens",
		//    column: "user_id",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_character_official_songs_characters_character_id",
		//    table: "character_official_songs",
		//    column: "character_id",
		//    principalTable: "characters",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_character_official_songs_official_songs_official_song_id",
		//    table: "character_official_songs",
		//    column: "official_song_id",
		//    principalTable: "official_songs",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_characters_official_games_origin_game_id",
		//    table: "characters",
		//    column: "origin_game_id",
		//    principalTable: "official_games",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_official_song_arrangement_song_arrangement_songs_arrangemen",
		//    table: "official_song_arrangement_song",
		//    column: "arrangement_song_id",
		//    principalTable: "arrangement_songs",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_official_song_arrangement_song_official_songs_official_song",
		//    table: "official_song_arrangement_song",
		//    column: "official_song_id",
		//    principalTable: "official_songs",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_official_songs_official_games_game_id",
		//    table: "official_songs",
		//    column: "game_id",
		//    principalTable: "official_games",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "fk_user_profiles_users_user_id1",
		//    table: "user_profiles",
		//    column: "user_id",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "id",
		//    onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		//migrationBuilder.DropForeignKey(
		//    name: "fk_arrangement_songs_circles_circle_id",
		//    table: "arrangement_songs");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_asp_net_role_claims_asp_net_roles_role_id",
		//    table: "AspNetRoleClaims");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_asp_net_user_claims_asp_net_users_user_id",
		//    table: "AspNetUserClaims");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_asp_net_user_logins_asp_net_users_user_id",
		//    table: "AspNetUserLogins");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_asp_net_user_roles_asp_net_roles_role_id",
		//    table: "AspNetUserRoles");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_asp_net_user_roles_asp_net_users_user_id",
		//    table: "AspNetUserRoles");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_asp_net_user_tokens_asp_net_users_user_id",
		//    table: "AspNetUserTokens");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_character_official_songs_characters_character_id",
		//    table: "character_official_songs");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_character_official_songs_official_songs_official_song_id",
		//    table: "character_official_songs");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_characters_official_games_origin_game_id",
		//    table: "characters");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_official_song_arrangement_song_arrangement_songs_arrangemen",
		//    table: "official_song_arrangement_song");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_official_song_arrangement_song_official_songs_official_song",
		//    table: "official_song_arrangement_song");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_official_songs_official_games_game_id",
		//    table: "official_songs");

		//migrationBuilder.DropForeignKey(
		//    name: "fk_user_profiles_users_user_id1",
		//    table: "user_profiles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_circles",
		//    table: "circles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_characters",
		//    table: "characters");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_asp_net_user_tokens",
		//    table: "AspNetUserTokens");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_asp_net_users",
		//    table: "AspNetUsers");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_asp_net_user_roles",
		//    table: "AspNetUserRoles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_asp_net_user_logins",
		//    table: "AspNetUserLogins");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_asp_net_user_claims",
		//    table: "AspNetUserClaims");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_asp_net_roles",
		//    table: "AspNetRoles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_asp_net_role_claims",
		//    table: "AspNetRoleClaims");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_user_profiles",
		//    table: "user_profiles");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_official_songs",
		//    table: "official_songs");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_official_song_arrangement_song",
		//    table: "official_song_arrangement_song");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_official_games",
		//    table: "official_games");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_character_official_songs",
		//    table: "character_official_songs");

		//migrationBuilder.DropPrimaryKey(
		//    name: "pk_arrangement_songs",
		//    table: "arrangement_songs");

		migrationBuilder.RenameTable(
			name: "circles",
			newName: "Circles");

		migrationBuilder.RenameTable(
			name: "characters",
			newName: "Characters");

		migrationBuilder.RenameTable(
			name: "user_profiles",
			newName: "UserProfiles");

		migrationBuilder.RenameTable(
			name: "official_songs",
			newName: "OfficialSongs");

		migrationBuilder.RenameTable(
			name: "official_song_arrangement_song",
			newName: "OfficialSongArrangementSong");

		migrationBuilder.RenameTable(
			name: "official_games",
			newName: "OfficialGames");

		migrationBuilder.RenameTable(
			name: "character_official_songs",
			newName: "CharacterOfficialSongs");

		migrationBuilder.RenameTable(
			name: "arrangement_songs",
			newName: "ArrangementSongs");

		migrationBuilder.RenameColumn(
			name: "status",
			table: "Circles",
			newName: "Status");

		migrationBuilder.RenameColumn(
			name: "name",
			table: "Circles",
			newName: "Name");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "Circles",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "name",
			table: "Characters",
			newName: "Name");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "Characters",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "origin_game_id",
			table: "Characters",
			newName: "OriginGameId");

		migrationBuilder.RenameColumn(
			name: "image_url",
			table: "Characters",
			newName: "ImageUrl");

		migrationBuilder.RenameIndex(
			name: "ix_characters_origin_game_id",
			table: "Characters",
			newName: "IX_Characters_OriginGameId");

		migrationBuilder.RenameColumn(
			name: "value",
			table: "AspNetUserTokens",
			newName: "Value");

		migrationBuilder.RenameColumn(
			name: "name",
			table: "AspNetUserTokens",
			newName: "Name");

		migrationBuilder.RenameColumn(
			name: "login_provider",
			table: "AspNetUserTokens",
			newName: "LoginProvider");

		migrationBuilder.RenameColumn(
			name: "user_id",
			table: "AspNetUserTokens",
			newName: "UserId");

		migrationBuilder.RenameColumn(
			name: "email",
			table: "AspNetUsers",
			newName: "Email");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "AspNetUsers",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "user_name",
			table: "AspNetUsers",
			newName: "UserName");

		migrationBuilder.RenameColumn(
			name: "two_factor_enabled",
			table: "AspNetUsers",
			newName: "TwoFactorEnabled");

		migrationBuilder.RenameColumn(
			name: "security_stamp",
			table: "AspNetUsers",
			newName: "SecurityStamp");

		migrationBuilder.RenameColumn(
			name: "profile_id",
			table: "AspNetUsers",
			newName: "ProfileId");

		migrationBuilder.RenameColumn(
			name: "phone_number_confirmed",
			table: "AspNetUsers",
			newName: "PhoneNumberConfirmed");

		migrationBuilder.RenameColumn(
			name: "phone_number",
			table: "AspNetUsers",
			newName: "PhoneNumber");

		migrationBuilder.RenameColumn(
			name: "password_hash",
			table: "AspNetUsers",
			newName: "PasswordHash");

		migrationBuilder.RenameColumn(
			name: "normalized_user_name",
			table: "AspNetUsers",
			newName: "NormalizedUserName");

		migrationBuilder.RenameColumn(
			name: "normalized_email",
			table: "AspNetUsers",
			newName: "NormalizedEmail");

		migrationBuilder.RenameColumn(
			name: "lockout_end",
			table: "AspNetUsers",
			newName: "LockoutEnd");

		migrationBuilder.RenameColumn(
			name: "lockout_enabled",
			table: "AspNetUsers",
			newName: "LockoutEnabled");

		migrationBuilder.RenameColumn(
			name: "email_confirmed",
			table: "AspNetUsers",
			newName: "EmailConfirmed");

		migrationBuilder.RenameColumn(
			name: "concurrency_stamp",
			table: "AspNetUsers",
			newName: "ConcurrencyStamp");

		migrationBuilder.RenameColumn(
			name: "access_failed_count",
			table: "AspNetUsers",
			newName: "AccessFailedCount");

		migrationBuilder.RenameColumn(
			name: "role_id",
			table: "AspNetUserRoles",
			newName: "RoleId");

		migrationBuilder.RenameColumn(
			name: "user_id",
			table: "AspNetUserRoles",
			newName: "UserId");

		migrationBuilder.RenameIndex(
			name: "ix_asp_net_user_roles_role_id",
			table: "AspNetUserRoles",
			newName: "IX_AspNetUserRoles_RoleId");

		migrationBuilder.RenameColumn(
			name: "user_id",
			table: "AspNetUserLogins",
			newName: "UserId");

		migrationBuilder.RenameColumn(
			name: "provider_display_name",
			table: "AspNetUserLogins",
			newName: "ProviderDisplayName");

		migrationBuilder.RenameColumn(
			name: "provider_key",
			table: "AspNetUserLogins",
			newName: "ProviderKey");

		migrationBuilder.RenameColumn(
			name: "login_provider",
			table: "AspNetUserLogins",
			newName: "LoginProvider");

		migrationBuilder.RenameIndex(
			name: "ix_asp_net_user_logins_user_id",
			table: "AspNetUserLogins",
			newName: "IX_AspNetUserLogins_UserId");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "AspNetUserClaims",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "user_id",
			table: "AspNetUserClaims",
			newName: "UserId");

		migrationBuilder.RenameColumn(
			name: "claim_value",
			table: "AspNetUserClaims",
			newName: "ClaimValue");

		migrationBuilder.RenameColumn(
			name: "claim_type",
			table: "AspNetUserClaims",
			newName: "ClaimType");

		migrationBuilder.RenameIndex(
			name: "ix_asp_net_user_claims_user_id",
			table: "AspNetUserClaims",
			newName: "IX_AspNetUserClaims_UserId");

		migrationBuilder.RenameColumn(
			name: "name",
			table: "AspNetRoles",
			newName: "Name");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "AspNetRoles",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "normalized_name",
			table: "AspNetRoles",
			newName: "NormalizedName");

		migrationBuilder.RenameColumn(
			name: "concurrency_stamp",
			table: "AspNetRoles",
			newName: "ConcurrencyStamp");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "AspNetRoleClaims",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "role_id",
			table: "AspNetRoleClaims",
			newName: "RoleId");

		migrationBuilder.RenameColumn(
			name: "claim_value",
			table: "AspNetRoleClaims",
			newName: "ClaimValue");

		migrationBuilder.RenameColumn(
			name: "claim_type",
			table: "AspNetRoleClaims",
			newName: "ClaimType");

		migrationBuilder.RenameIndex(
			name: "ix_asp_net_role_claims_role_id",
			table: "AspNetRoleClaims",
			newName: "IX_AspNetRoleClaims_RoleId");

		migrationBuilder.RenameColumn(
			name: "bio",
			table: "UserProfiles",
			newName: "Bio");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "UserProfiles",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "user_id",
			table: "UserProfiles",
			newName: "UserId");

		migrationBuilder.RenameColumn(
			name: "avatar_url",
			table: "UserProfiles",
			newName: "AvatarUrl");

		migrationBuilder.RenameIndex(
			name: "ix_user_profiles_user_id",
			table: "UserProfiles",
			newName: "IX_UserProfiles_UserId");

		migrationBuilder.RenameColumn(
			name: "title",
			table: "OfficialSongs",
			newName: "Title");

		migrationBuilder.RenameColumn(
			name: "context",
			table: "OfficialSongs",
			newName: "Context");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "OfficialSongs",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "game_id",
			table: "OfficialSongs",
			newName: "GameId");

		migrationBuilder.RenameIndex(
			name: "ix_official_songs_game_id",
			table: "OfficialSongs",
			newName: "IX_OfficialSongs_GameId");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "OfficialSongArrangementSong",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "official_song_id",
			table: "OfficialSongArrangementSong",
			newName: "OfficialSongId");

		migrationBuilder.RenameColumn(
			name: "arrangement_song_id",
			table: "OfficialSongArrangementSong",
			newName: "ArrangementSongId");

		migrationBuilder.RenameIndex(
			name: "ix_official_song_arrangement_song_official_song_id",
			table: "OfficialSongArrangementSong",
			newName: "IX_OfficialSongArrangementSong_OfficialSongId");

		migrationBuilder.RenameIndex(
			name: "ix_official_song_arrangement_song_arrangement_song_id",
			table: "OfficialSongArrangementSong",
			newName: "IX_OfficialSongArrangementSong_ArrangementSongId");

		migrationBuilder.RenameColumn(
			name: "title",
			table: "OfficialGames",
			newName: "Title");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "OfficialGames",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "release_date",
			table: "OfficialGames",
			newName: "ReleaseDate");

		migrationBuilder.RenameColumn(
			name: "number_code",
			table: "OfficialGames",
			newName: "NumberCode");

		migrationBuilder.RenameColumn(
			name: "image_url",
			table: "OfficialGames",
			newName: "ImageUrl");

		migrationBuilder.RenameColumn(
			name: "game_code",
			table: "OfficialGames",
			newName: "GameCode");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "CharacterOfficialSongs",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "official_song_id",
			table: "CharacterOfficialSongs",
			newName: "OfficialSongId");

		migrationBuilder.RenameColumn(
			name: "character_id",
			table: "CharacterOfficialSongs",
			newName: "CharacterId");

		migrationBuilder.RenameIndex(
			name: "ix_character_official_songs_official_song_id",
			table: "CharacterOfficialSongs",
			newName: "IX_CharacterOfficialSongs_OfficialSongId");

		migrationBuilder.RenameIndex(
			name: "ix_character_official_songs_character_id",
			table: "CharacterOfficialSongs",
			newName: "IX_CharacterOfficialSongs_CharacterId");

		migrationBuilder.RenameColumn(
			name: "url",
			table: "ArrangementSongs",
			newName: "Url");

		migrationBuilder.RenameColumn(
			name: "title",
			table: "ArrangementSongs",
			newName: "Title");

		migrationBuilder.RenameColumn(
			name: "status",
			table: "ArrangementSongs",
			newName: "Status");

		migrationBuilder.RenameColumn(
			name: "id",
			table: "ArrangementSongs",
			newName: "Id");

		migrationBuilder.RenameColumn(
			name: "title_romaji",
			table: "ArrangementSongs",
			newName: "TitleRomaji");

		migrationBuilder.RenameColumn(
			name: "title_japanese",
			table: "ArrangementSongs",
			newName: "TitleJapanese");

		migrationBuilder.RenameColumn(
			name: "circle_id",
			table: "ArrangementSongs",
			newName: "CircleId");

		migrationBuilder.RenameIndex(
			name: "ix_arrangement_songs_circle_id",
			table: "ArrangementSongs",
			newName: "IX_ArrangementSongs_CircleId");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_Circles",
		//    table: "Circles",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_Characters",
		//    table: "Characters",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_AspNetUserTokens",
		//    table: "AspNetUserTokens",
		//    columns: new[] { "UserId", "LoginProvider", "Name" });

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_AspNetUsers",
		//    table: "AspNetUsers",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_AspNetUserRoles",
		//    table: "AspNetUserRoles",
		//    columns: new[] { "UserId", "RoleId" });

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_AspNetUserLogins",
		//    table: "AspNetUserLogins",
		//    columns: new[] { "LoginProvider", "ProviderKey" });

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_AspNetUserClaims",
		//    table: "AspNetUserClaims",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_AspNetRoles",
		//    table: "AspNetRoles",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_AspNetRoleClaims",
		//    table: "AspNetRoleClaims",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_UserProfiles",
		//    table: "UserProfiles",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_OfficialSongs",
		//    table: "OfficialSongs",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_OfficialSongArrangementSong",
		//    table: "OfficialSongArrangementSong",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_OfficialGames",
		//    table: "OfficialGames",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_CharacterOfficialSongs",
		//    table: "CharacterOfficialSongs",
		//    column: "Id");

		//migrationBuilder.AddPrimaryKey(
		//    name: "PK_ArrangementSongs",
		//    table: "ArrangementSongs",
		//    column: "Id");

		//migrationBuilder.AddForeignKey(
		//    name: "FK_ArrangementSongs_Circles_CircleId",
		//    table: "ArrangementSongs",
		//    column: "CircleId",
		//    principalTable: "Circles",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
		//    table: "AspNetRoleClaims",
		//    column: "RoleId",
		//    principalTable: "AspNetRoles",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
		//    table: "AspNetUserClaims",
		//    column: "UserId",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
		//    table: "AspNetUserLogins",
		//    column: "UserId",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
		//    table: "AspNetUserRoles",
		//    column: "RoleId",
		//    principalTable: "AspNetRoles",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
		//    table: "AspNetUserRoles",
		//    column: "UserId",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
		//    table: "AspNetUserTokens",
		//    column: "UserId",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_CharacterOfficialSongs_Characters_CharacterId",
		//    table: "CharacterOfficialSongs",
		//    column: "CharacterId",
		//    principalTable: "Characters",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_CharacterOfficialSongs_OfficialSongs_OfficialSongId",
		//    table: "CharacterOfficialSongs",
		//    column: "OfficialSongId",
		//    principalTable: "OfficialSongs",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_Characters_OfficialGames_OriginGameId",
		//    table: "Characters",
		//    column: "OriginGameId",
		//    principalTable: "OfficialGames",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_OfficialSongArrangementSong_ArrangementSongs_ArrangementSon~",
		//    table: "OfficialSongArrangementSong",
		//    column: "ArrangementSongId",
		//    principalTable: "ArrangementSongs",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_OfficialSongArrangementSong_OfficialSongs_OfficialSongId",
		//    table: "OfficialSongArrangementSong",
		//    column: "OfficialSongId",
		//    principalTable: "OfficialSongs",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_OfficialSongs_OfficialGames_GameId",
		//    table: "OfficialSongs",
		//    column: "GameId",
		//    principalTable: "OfficialGames",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);

		//migrationBuilder.AddForeignKey(
		//    name: "FK_UserProfiles_AspNetUsers_UserId",
		//    table: "UserProfiles",
		//    column: "UserId",
		//    principalTable: "AspNetUsers",
		//    principalColumn: "Id",
		//    onDelete: ReferentialAction.Cascade);
	}
}
