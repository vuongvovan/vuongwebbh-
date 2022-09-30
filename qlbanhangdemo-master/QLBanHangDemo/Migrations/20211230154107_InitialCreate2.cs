using Microsoft.EntityFrameworkCore.Migrations;

namespace QLBanHangDemo.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CUSTOMERS_PASSWORD",
                table: "CUSTOMERS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CUSTOMERS_PASSWORD",
                table: "CUSTOMERS",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
