using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations;

/// <inheritdoc />
public partial class Add_CircleArrangementSong_1N_Relation : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Circles",
			columns: table => new
			{
				Id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				Name = table.Column<string>(type: "text", nullable: false),
				Status = table.Column<int>(type: "integer", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Circles", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "ArrangementSongs",
			columns: table => new
			{
				Id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				Title = table.Column<string>(type: "text", nullable: false),
				Status = table.Column<int>(type: "integer", nullable: false),
				CircleId = table.Column<int>(type: "integer", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ArrangementSongs", x => x.Id);
				table.ForeignKey(
					name: "FK_ArrangementSongs_Circles_CircleId",
					column: x => x.CircleId,
					principalTable: "Circles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "OfficialSongArrangementSong",
			columns: table => new
			{
				Id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				OfficialSongId = table.Column<int>(type: "integer", nullable: false),
				ArrangementSongId = table.Column<int>(type: "integer", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_OfficialSongArrangementSong", x => x.Id);
				table.ForeignKey(
					name: "FK_OfficialSongArrangementSong_ArrangementSongs_ArSongId",
					column: x => x.ArrangementSongId,
					principalTable: "ArrangementSongs",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_OfficialSongArrangementSong_OfficialSongs_OfficialSongId",
					column: x => x.OfficialSongId,
					principalTable: "OfficialSongs",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_ArrangementSongs_CircleId",
			table: "ArrangementSongs",
			column: "CircleId");

		migrationBuilder.CreateIndex(
			name: "IX_OfficialSongArrangementSong_ArrangementSongId",
			table: "OfficialSongArrangementSong",
			column: "ArrangementSongId");

		migrationBuilder.CreateIndex(
			name: "IX_OfficialSongArrangementSong_OfficialSongId",
			table: "OfficialSongArrangementSong",
			column: "OfficialSongId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "OfficialSongArrangementSong");

		migrationBuilder.DropTable(
			name: "ArrangementSongs");

		migrationBuilder.DropTable(
			name: "Circles");
	}
}
