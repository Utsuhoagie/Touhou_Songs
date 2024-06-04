using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations;

/// <inheritdoc />
public partial class Fix_TierList_ArSong_BaseAudited : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "fk_user_profiles_users_user_id1",
			table: "user_profiles");

		migrationBuilder.DropTable(
			name: "tier_list_items");

		migrationBuilder.DropTable(
			name: "tier_list_tiers");

		migrationBuilder.CreateSequence(
			name: "BaseEntitySequence");

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "tier_lists",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "arrangement_songs",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AddColumn<string>(
			name: "created_by_user_name",
			table: "arrangement_songs",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<DateTime>(
			name: "created_on",
			table: "arrangement_songs",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

		migrationBuilder.AddColumn<string>(
			name: "updated_by_user_name",
			table: "arrangement_songs",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<DateTime>(
			name: "updated_on",
			table: "arrangement_songs",
			type: "timestamp with time zone",
			nullable: true);

		migrationBuilder.AddForeignKey(
			name: "fk_user_profiles_asp_net_users_user_id",
			table: "user_profiles",
			column: "user_id",
			principalTable: "AspNetUsers",
			principalColumn: "id",
			onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "fk_user_profiles_asp_net_users_user_id",
			table: "user_profiles");

		migrationBuilder.DropColumn(
			name: "created_by_user_name",
			table: "arrangement_songs");

		migrationBuilder.DropColumn(
			name: "created_on",
			table: "arrangement_songs");

		migrationBuilder.DropColumn(
			name: "updated_by_user_name",
			table: "arrangement_songs");

		migrationBuilder.DropColumn(
			name: "updated_on",
			table: "arrangement_songs");

		migrationBuilder.DropSequence(
			name: "BaseEntitySequence");

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "tier_lists",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "arrangement_songs",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.CreateTable(
			name: "tier_list_tiers",
			columns: table => new
			{
				id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				tier_list_id = table.Column<int>(type: "integer", nullable: false),
				label = table.Column<string>(type: "text", nullable: false),
				order = table.Column<int>(type: "integer", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_tier_list_tiers", x => x.id);
				table.ForeignKey(
					name: "fk_tier_list_tiers_tier_lists_tier_list_id",
					column: x => x.tier_list_id,
					principalTable: "tier_lists",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "tier_list_items",
			columns: table => new
			{
				id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				tier_list_tier_id = table.Column<int>(type: "integer", nullable: false),
				icon_url = table.Column<string>(type: "text", nullable: false),
				label = table.Column<string>(type: "text", nullable: false),
				order = table.Column<int>(type: "integer", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_tier_list_items", x => x.id);
				table.ForeignKey(
					name: "fk_tier_list_items_tier_list_tiers_tier_list_tier_id",
					column: x => x.tier_list_tier_id,
					principalTable: "tier_list_tiers",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "ix_tier_list_items_tier_list_tier_id",
			table: "tier_list_items",
			column: "tier_list_tier_id");

		migrationBuilder.CreateIndex(
			name: "ix_tier_list_tiers_tier_list_id",
			table: "tier_list_tiers",
			column: "tier_list_id");

		migrationBuilder.AddForeignKey(
			name: "fk_user_profiles_users_user_id1",
			table: "user_profiles",
			column: "user_id",
			principalTable: "AspNetUsers",
			principalColumn: "id",
			onDelete: ReferentialAction.Cascade);
	}
}
