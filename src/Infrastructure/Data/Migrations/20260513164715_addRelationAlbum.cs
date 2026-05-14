using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addRelationAlbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "AlbumId",
                table: "Songs",
                newName: "albumId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                newName: "IX_Songs_albumId");

            migrationBuilder.AlterColumn<Guid>(
                name: "albumId",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_albumId",
                table: "Songs",
                column: "albumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_albumId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "albumId",
                table: "Songs",
                newName: "AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_albumId",
                table: "Songs",
                newName: "IX_Songs_AlbumId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlbumId",
                table: "Songs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");
        }
    }
}
