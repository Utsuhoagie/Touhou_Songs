using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
	/// <inheritdoc />
	public partial class Add_OfficialGame_GameCode : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "GameCode",
				table: "OfficialGames",
				type: "text",
				nullable: false,
				defaultValue: "??");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "GameCode",
				table: "OfficialGames");
		}
	}
}
