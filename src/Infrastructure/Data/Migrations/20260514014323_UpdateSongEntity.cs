using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSongEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Songs",
                table: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_IdAlbum",
                table: "Songs",
                column: "IdAlbum");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_IdAlbum",
                table: "Songs",
                column: "IdAlbum",
                principalTable: "Albums",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_IdAlbum",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_IdAlbum",
                table: "Songs");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Albums",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Songs",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
