using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_TierList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tier_lists",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_user_name = table.Column<string>(type: "text", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by_user_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tier_lists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tier_list_tiers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    tier_list_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tier_list_tiers", x => x.id);
                    table.ForeignKey(
                        name: "fk_tier_list_tiers_tier_lists_tier_list_id",
                        column: x => x.tier_list_id,
                        principalTable: "tier_lists",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tier_list_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    icon_url = table.Column<string>(type: "text", nullable: false),
                    tier_list_tier_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tier_list_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_tier_list_items_tier_list_tiers_tier_list_tier_id",
                        column: x => x.tier_list_tier_id,
                        principalTable: "tier_list_tiers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_tier_list_items_tier_list_tier_id",
                table: "tier_list_items",
                column: "tier_list_tier_id");

            migrationBuilder.CreateIndex(
                name: "ix_tier_list_tiers_tier_list_id",
                table: "tier_list_tiers",
                column: "tier_list_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tier_list_items");

            migrationBuilder.DropTable(
                name: "tier_list_tiers");

            migrationBuilder.DropTable(
                name: "tier_lists");
        }
    }
}
