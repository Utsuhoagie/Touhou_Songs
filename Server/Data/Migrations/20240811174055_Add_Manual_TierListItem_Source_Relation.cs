using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Manual_TierListItem_Source_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "arrangement_song_id",
                table: "tier_list_items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "official_game_id",
                table: "tier_list_items",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_tier_list_items_arrangement_song_id",
                table: "tier_list_items",
                column: "arrangement_song_id");

            migrationBuilder.CreateIndex(
                name: "ix_tier_list_items_official_game_id",
                table: "tier_list_items",
                column: "official_game_id");

            migrationBuilder.AddForeignKey(
                name: "fk_tier_list_items_arrangement_songs_arrangement_song_id",
                table: "tier_list_items",
                column: "arrangement_song_id",
                principalTable: "arrangement_songs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_tier_list_items_official_games_official_game_id",
                table: "tier_list_items",
                column: "official_game_id",
                principalTable: "official_games",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tier_list_items_arrangement_songs_arrangement_song_id",
                table: "tier_list_items");

            migrationBuilder.DropForeignKey(
                name: "fk_tier_list_items_official_games_official_game_id",
                table: "tier_list_items");

            migrationBuilder.DropIndex(
                name: "ix_tier_list_items_arrangement_song_id",
                table: "tier_list_items");

            migrationBuilder.DropIndex(
                name: "ix_tier_list_items_official_game_id",
                table: "tier_list_items");

            migrationBuilder.DropColumn(
                name: "arrangement_song_id",
                table: "tier_list_items");

            migrationBuilder.DropColumn(
                name: "official_game_id",
                table: "tier_list_items");
        }
    }
}
