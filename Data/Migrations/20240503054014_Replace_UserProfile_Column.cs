using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Replace_UserProfile_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "UserProfiles",
                newName: "Bio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "UserProfiles",
                newName: "DisplayName");
        }
    }
}
