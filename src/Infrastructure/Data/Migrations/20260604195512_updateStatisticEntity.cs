using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateStatisticEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteGender",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "SongTop",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "TotalReproductioByGender",
                table: "Statistics",
                newName: "Reproductions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reproductions",
                table: "Statistics",
                newName: "TotalReproductioByGender");

            migrationBuilder.AddColumn<string>(
                name: "FavoriteGender",
                table: "Statistics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SongTop",
                table: "Statistics",
                type: "TEXT",
                nullable: true);
        }
    }
}
