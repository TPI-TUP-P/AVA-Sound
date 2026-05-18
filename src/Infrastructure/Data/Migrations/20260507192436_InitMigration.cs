using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "Reviews",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "idSong",
                table: "Reviews",
                newName: "IdSong");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Reviews",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reviews",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Reviews",
                newName: "idUser");

            migrationBuilder.RenameColumn(
                name: "IdSong",
                table: "Reviews",
                newName: "idSong");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Reviews",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reviews",
                newName: "id");
        }
    }
}
