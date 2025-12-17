using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Project_Kredit.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToCreditRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CreditRegistrations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CreditRegistrations");
        }
    }
}
