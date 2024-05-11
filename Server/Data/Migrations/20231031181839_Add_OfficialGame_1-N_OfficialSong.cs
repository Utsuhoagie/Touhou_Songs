using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_OfficialGame_1N_OfficialSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "OfficialSongs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OfficialGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialGames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfficialSongs_GameId",
                table: "OfficialSongs",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialSongs_OfficialGames_GameId",
                table: "OfficialSongs",
                column: "GameId",
                principalTable: "OfficialGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialSongs_OfficialGames_GameId",
                table: "OfficialSongs");

            migrationBuilder.DropTable(
                name: "OfficialGames");

            migrationBuilder.DropIndex(
                name: "IX_OfficialSongs_GameId",
                table: "OfficialSongs");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "OfficialSongs");
        }
    }
}
