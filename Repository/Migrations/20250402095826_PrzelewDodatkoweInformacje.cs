using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class PrzelewDodatkoweInformacje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DodanyDnia",
                table: "Przelewy",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlikPochodzenia",
                table: "Przelewy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrybDodania",
                table: "Przelewy",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DodanyDnia",
                table: "Przelewy");

            migrationBuilder.DropColumn(
                name: "PlikPochodzenia",
                table: "Przelewy");

            migrationBuilder.DropColumn(
                name: "TrybDodania",
                table: "Przelewy");
        }
    }
}
