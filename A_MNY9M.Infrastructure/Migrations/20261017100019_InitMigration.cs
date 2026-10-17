using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A_MNY9M.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auto_voice_channels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    leader_discord_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    discord_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auto_voice_channels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "source_voice_channels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    discord_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    InitiatorDiscordId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    GuildCategoryId = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SetAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_source_voice_channels", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auto_voice_channels");

            migrationBuilder.DropTable(
                name: "source_voice_channels");
        }
    }
}
