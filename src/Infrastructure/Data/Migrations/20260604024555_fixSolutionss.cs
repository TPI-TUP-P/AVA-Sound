using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixSolutionss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalReproductions",
                table: "Statistics");

            migrationBuilder.AlterColumn<Guid>(
                name: "SongTop",
                table: "Statistics",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "TotalReproductioByGender",
                table: "Statistics",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_IdUser",
                table: "Statistics",
                column: "IdUser",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Users_IdUser",
                table: "Statistics",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Users_IdUser",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_IdUser",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TotalReproductioByGender",
                table: "Statistics");

            migrationBuilder.AlterColumn<Guid>(
                name: "SongTop",
                table: "Statistics",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalReproductions",
                table: "Statistics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
