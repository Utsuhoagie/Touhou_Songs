using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_TierListType_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "tier_lists",
                type: "text",
                nullable: false,
                defaultValue: "ArrangementSongs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "tier_lists");
        }
    }
}
