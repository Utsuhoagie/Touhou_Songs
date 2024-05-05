using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserProfile_AvatarUrl_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "UserProfiles",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "UserProfiles");
        }
    }
}
