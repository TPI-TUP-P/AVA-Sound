using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addRelationAlbumS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_albumId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_albumId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "albumId",
                table: "Songs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "albumId",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Songs_albumId",
                table: "Songs",
                column: "albumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_albumId",
                table: "Songs",
                column: "albumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
