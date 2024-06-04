using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_TierList_Cascades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tier_list_items_tier_list_tiers_tier_list_tier_id",
                table: "tier_list_items");

            migrationBuilder.DropForeignKey(
                name: "fk_tier_list_tiers_tier_lists_tier_list_id",
                table: "tier_list_tiers");

            migrationBuilder.AlterColumn<int>(
                name: "tier_list_id",
                table: "tier_list_tiers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "tier_list_tier_id",
                table: "tier_list_items",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_tier_list_items_tier_list_tiers_tier_list_tier_id",
                table: "tier_list_items",
                column: "tier_list_tier_id",
                principalTable: "tier_list_tiers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tier_list_tiers_tier_lists_tier_list_id",
                table: "tier_list_tiers",
                column: "tier_list_id",
                principalTable: "tier_lists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tier_list_items_tier_list_tiers_tier_list_tier_id",
                table: "tier_list_items");

            migrationBuilder.DropForeignKey(
                name: "fk_tier_list_tiers_tier_lists_tier_list_id",
                table: "tier_list_tiers");

            migrationBuilder.AlterColumn<int>(
                name: "tier_list_id",
                table: "tier_list_tiers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "tier_list_tier_id",
                table: "tier_list_items",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_tier_list_items_tier_list_tiers_tier_list_tier_id",
                table: "tier_list_items",
                column: "tier_list_tier_id",
                principalTable: "tier_list_tiers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_tier_list_tiers_tier_lists_tier_list_id",
                table: "tier_list_tiers",
                column: "tier_list_id",
                principalTable: "tier_lists",
                principalColumn: "id");
        }
    }
}
