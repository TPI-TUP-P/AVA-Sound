using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_ReproductionsLists_ReproductionsListId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ReproductionsListId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ReproductionsListId",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "ReproductionsListSong",
                columns: table => new
                {
                    ReproductionsListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReproductionsListSong", x => new { x.ReproductionsListId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_ReproductionsListSong_ReproductionsLists_ReproductionsListId",
                        column: x => x.ReproductionsListId,
                        principalTable: "ReproductionsLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReproductionsListSong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReproductionsListSong_SongsId",
                table: "ReproductionsListSong",
                column: "SongsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReproductionsListSong");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ReproductionsListId",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ReproductionsListId",
                table: "Songs",
                column: "ReproductionsListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_ReproductionsLists_ReproductionsListId",
                table: "Songs",
                column: "ReproductionsListId",
                principalTable: "ReproductionsLists",
                principalColumn: "Id");
        }
    }
}
