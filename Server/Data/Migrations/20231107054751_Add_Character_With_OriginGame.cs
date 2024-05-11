using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Character_With_OriginGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "OfficialSongs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    OriginGameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_OfficialGames_OriginGameId",
                        column: x => x.OriginGameId,
                        principalTable: "OfficialGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfficialSongs_CharacterId",
                table: "OfficialSongs",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_OriginGameId",
                table: "Characters",
                column: "OriginGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialSongs_Characters_CharacterId",
                table: "OfficialSongs",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialSongs_Characters_CharacterId",
                table: "OfficialSongs");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_OfficialSongs_CharacterId",
                table: "OfficialSongs");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "OfficialSongs");
        }
    }
}
