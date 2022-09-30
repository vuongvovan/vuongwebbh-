using Microsoft.EntityFrameworkCore.Migrations;

namespace QLBanHangDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CUSTOMERS_ADDRESS",
                table: "CUSTOMERS");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMERS_EMAIL",
                table: "CUSTOMERS",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMERS_PASSWORD",
                table: "CUSTOMERS",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CUSTOMERS_EMAIL",
                table: "CUSTOMERS");

            migrationBuilder.DropColumn(
                name: "CUSTOMERS_PASSWORD",
                table: "CUSTOMERS");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMERS_ADDRESS",
                table: "CUSTOMERS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
