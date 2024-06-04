using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations;

/// <inheritdoc />
public partial class Refactor_Audited_Remaining : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "user_profiles",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AddColumn<string>(
			name: "created_by_user_name",
			table: "user_profiles",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<DateTime>(
			name: "created_on",
			table: "user_profiles",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

		migrationBuilder.AddColumn<string>(
			name: "updated_by_user_name",
			table: "user_profiles",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<DateTime>(
			name: "updated_on",
			table: "user_profiles",
			type: "timestamp with time zone",
			nullable: true);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "official_songs",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AddColumn<string>(
			name: "created_by_user_name",
			table: "official_songs",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<DateTime>(
			name: "created_on",
			table: "official_songs",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

		migrationBuilder.AddColumn<string>(
			name: "updated_by_user_name",
			table: "official_songs",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<DateTime>(
			name: "updated_on",
			table: "official_songs",
			type: "timestamp with time zone",
			nullable: true);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "official_games",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AddColumn<string>(
			name: "created_by_user_name",
			table: "official_games",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<DateTime>(
			name: "created_on",
			table: "official_games",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

		migrationBuilder.AddColumn<string>(
			name: "updated_by_user_name",
			table: "official_games",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<DateTime>(
			name: "updated_on",
			table: "official_games",
			type: "timestamp with time zone",
			nullable: true);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "circles",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AddColumn<string>(
			name: "created_by_user_name",
			table: "circles",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<DateTime>(
			name: "created_on",
			table: "circles",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

		migrationBuilder.AddColumn<string>(
			name: "updated_by_user_name",
			table: "circles",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<DateTime>(
			name: "updated_on",
			table: "circles",
			type: "timestamp with time zone",
			nullable: true);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "characters",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AddColumn<string>(
			name: "created_by_user_name",
			table: "characters",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<DateTime>(
			name: "created_on",
			table: "characters",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

		migrationBuilder.AddColumn<string>(
			name: "updated_by_user_name",
			table: "characters",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<DateTime>(
			name: "updated_on",
			table: "characters",
			type: "timestamp with time zone",
			nullable: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "created_by_user_name",
			table: "user_profiles");

		migrationBuilder.DropColumn(
			name: "created_on",
			table: "user_profiles");

		migrationBuilder.DropColumn(
			name: "updated_by_user_name",
			table: "user_profiles");

		migrationBuilder.DropColumn(
			name: "updated_on",
			table: "user_profiles");

		migrationBuilder.DropColumn(
			name: "created_by_user_name",
			table: "official_songs");

		migrationBuilder.DropColumn(
			name: "created_on",
			table: "official_songs");

		migrationBuilder.DropColumn(
			name: "updated_by_user_name",
			table: "official_songs");

		migrationBuilder.DropColumn(
			name: "updated_on",
			table: "official_songs");

		migrationBuilder.DropColumn(
			name: "created_by_user_name",
			table: "official_games");

		migrationBuilder.DropColumn(
			name: "created_on",
			table: "official_games");

		migrationBuilder.DropColumn(
			name: "updated_by_user_name",
			table: "official_games");

		migrationBuilder.DropColumn(
			name: "updated_on",
			table: "official_games");

		migrationBuilder.DropColumn(
			name: "created_by_user_name",
			table: "circles");

		migrationBuilder.DropColumn(
			name: "created_on",
			table: "circles");

		migrationBuilder.DropColumn(
			name: "updated_by_user_name",
			table: "circles");

		migrationBuilder.DropColumn(
			name: "updated_on",
			table: "circles");

		migrationBuilder.DropColumn(
			name: "created_by_user_name",
			table: "characters");

		migrationBuilder.DropColumn(
			name: "created_on",
			table: "characters");

		migrationBuilder.DropColumn(
			name: "updated_by_user_name",
			table: "characters");

		migrationBuilder.DropColumn(
			name: "updated_on",
			table: "characters");

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "user_profiles",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "official_songs",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "official_games",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "circles",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "characters",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
	}
}
