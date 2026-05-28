using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateFieldUser : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ReproductionsLists",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ReproductionsLists",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ReproductionsLists",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ReproductionsLists",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
