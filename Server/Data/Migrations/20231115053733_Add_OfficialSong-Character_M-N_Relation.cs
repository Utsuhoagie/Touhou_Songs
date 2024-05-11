using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
	/// <inheritdoc />
	public partial class Add_OfficialSongCharacter_MN_Relation : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			//migrationBuilder.DropForeignKey(
			//    name: "FK_OfficialSongs_Characters_CharacterId",
			//    table: "OfficialSongs");

			//migrationBuilder.DropIndex(
			//    name: "IX_OfficialSongs_CharacterId",
			//    table: "OfficialSongs");

			//migrationBuilder.DropColumn(
			//    name: "CharacterId",
			//    table: "OfficialSongs");

			migrationBuilder.CreateTable(
				name: "CharacterOfficialSongs",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					CharacterId = table.Column<int>(type: "integer", nullable: false),
					OfficialSongId = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CharacterOfficialSongs", x => x.Id);
					table.ForeignKey(
						name: "FK_CharacterOfficialSongs_Characters_CharacterId",
						column: x => x.CharacterId,
						principalTable: "Characters",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CharacterOfficialSongs_OfficialSongs_OfficialSongId",
						column: x => x.OfficialSongId,
						principalTable: "OfficialSongs",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_CharacterOfficialSongs_CharacterId",
				table: "CharacterOfficialSongs",
				column: "CharacterId");

			migrationBuilder.CreateIndex(
				name: "IX_CharacterOfficialSongs_OfficialSongId",
				table: "CharacterOfficialSongs",
				column: "OfficialSongId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "CharacterOfficialSongs");

			//    migrationBuilder.AddColumn<int>(
			//        name: "CharacterId",
			//        table: "OfficialSongs",
			//        type: "integer",
			//        nullable: true);

			//    migrationBuilder.CreateIndex(
			//        name: "IX_OfficialSongs_CharacterId",
			//        table: "OfficialSongs",
			//        column: "CharacterId");

			//    migrationBuilder.AddForeignKey(
			//        name: "FK_OfficialSongs_Characters_CharacterId",
			//        table: "OfficialSongs",
			//        column: "CharacterId",
			//        principalTable: "Characters",
			//        principalColumn: "Id");
		}
	}
}
