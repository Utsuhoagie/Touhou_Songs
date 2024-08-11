using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations;

/// <inheritdoc />
public partial class Fix_Sequence_PK : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex(
			name: "ix_tier_list_items_source_id",
			table: "tier_list_items");

		migrationBuilder.DropColumn(
			name: "label",
			table: "tier_list_items");

		migrationBuilder.DropColumn(
			name: "source_id",
			table: "tier_list_items");

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
			table: "tier_lists",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "tier_list_tiers",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "tier_list_items",
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

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "arrangement_songs",
			type: "integer",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer",
			oldDefaultValueSql: "nextval('\"BaseEntitySequence\"')")
			.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.DropSequence(
			name: "BaseEntitySequence");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateSequence(
			name: "BaseEntitySequence");

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "user_profiles",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "tier_lists",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "tier_list_tiers",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "tier_list_items",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AddColumn<string>(
			name: "label",
			table: "tier_list_items",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<int>(
			name: "source_id",
			table: "tier_list_items",
			type: "integer",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "official_songs",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "official_games",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "circles",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "characters",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.AlterColumn<int>(
			name: "id",
			table: "arrangement_songs",
			type: "integer",
			nullable: false,
			defaultValueSql: "nextval('\"BaseEntitySequence\"')",
			oldClrType: typeof(int),
			oldType: "integer")
			.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

		migrationBuilder.CreateIndex(
			name: "ix_tier_list_items_source_id",
			table: "tier_list_items",
			column: "source_id");
	}
}
