using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Project_Kredit.Migrations
{
    /// <inheritdoc />
    public partial class MakeDistrictIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "CreditRegistrations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CreditRegistrations",
                keyColumn: "DistrictId",
                keyValue: null,
                column: "DistrictId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "CreditRegistrations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
