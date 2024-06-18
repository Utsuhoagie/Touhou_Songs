using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_TierList_UserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_profile_id",
                table: "tier_lists",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_tier_lists_user_profile_id",
                table: "tier_lists",
                column: "user_profile_id");

            migrationBuilder.AddForeignKey(
                name: "fk_tier_lists_user_profiles_user_profile_id",
                table: "tier_lists",
                column: "user_profile_id",
                principalTable: "user_profiles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tier_lists_user_profiles_user_profile_id",
                table: "tier_lists");

            migrationBuilder.DropIndex(
                name: "ix_tier_lists_user_profile_id",
                table: "tier_lists");

            migrationBuilder.DropColumn(
                name: "user_profile_id",
                table: "tier_lists");
        }
    }
}
