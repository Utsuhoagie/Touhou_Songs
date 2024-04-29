using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_ArrangementSong_JapaneseTitle_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleJapanese",
                table: "ArrangementSongs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleRomaji",
                table: "ArrangementSongs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleJapanese",
                table: "ArrangementSongs");

            migrationBuilder.DropColumn(
                name: "TitleRomaji",
                table: "ArrangementSongs");
        }
    }
}
