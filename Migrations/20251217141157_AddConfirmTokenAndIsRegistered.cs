using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Project_Kredit.Migrations
{
    /// <inheritdoc />
    public partial class AddConfirmTokenAndIsRegistered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "isRegistered",
                table: "CreditRegistrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmToken",
                table: "CreditRegistrations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmTokenExpiry",
                table: "CreditRegistrations",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmToken",
                table: "CreditRegistrations");

            migrationBuilder.DropColumn(
                name: "ConfirmTokenExpiry",
                table: "CreditRegistrations");

            migrationBuilder.AlterColumn<bool>(
                name: "isRegistered",
                table: "CreditRegistrations",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
