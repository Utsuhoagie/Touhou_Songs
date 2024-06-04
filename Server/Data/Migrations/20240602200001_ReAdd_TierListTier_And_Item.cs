using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations;

/// <inheritdoc />
public partial class ReAdd_TierListTier_And_Item : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "tier_list_tiers",
			columns: table => new
			{
				id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"BaseEntitySequence\"')"),
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
				id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"BaseEntitySequence\"')"),
				tier_list_tier_id = table.Column<int>(type: "integer", nullable: false),
				label = table.Column<string>(type: "text", nullable: false),
				order = table.Column<int>(type: "integer", nullable: false),
				icon_url = table.Column<string>(type: "text", nullable: false),
				source_id = table.Column<int>(type: "integer", nullable: false)
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
			name: "ix_tier_list_items_source_id",
			table: "tier_list_items",
			column: "source_id");

		migrationBuilder.CreateIndex(
			name: "ix_tier_list_items_tier_list_tier_id",
			table: "tier_list_items",
			column: "tier_list_tier_id");

		migrationBuilder.CreateIndex(
			name: "ix_tier_list_tiers_tier_list_id",
			table: "tier_list_tiers",
			column: "tier_list_id");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "tier_list_items");

		migrationBuilder.DropTable(
			name: "tier_list_tiers");
	}
}
