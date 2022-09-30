using Microsoft.EntityFrameworkCore.Migrations;

namespace QLBanHangDemo.Migrations
{
    public partial class my : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BRAND",
                columns: table => new
                {
                    BRAND_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BRAND_NAME = table.Column<string>(maxLength: 100, nullable: false),
                    BRAND_DESC = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRAND", x => x.BRAND_ID);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    CATEGORY_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORY_NAME = table.Column<string>(maxLength: 100, nullable: true),
                    CATEGORY_DESC = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.CATEGORY_ID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    CUSTOMERS_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSTOMERS_NAME = table.Column<string>(maxLength: 50, nullable: false),
                    CUSTOMERS_PHONE = table.Column<string>(unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CUSTOMERS_ADDRESS = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER", x => x.CUSTOMERS_ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT",
                columns: table => new
                {
                    PAYMENT_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PAYMENT_METHOD = table.Column<string>(maxLength: 250, nullable: false),
                    PAYMENT_STATUS = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT", x => x.PAYMENT_ID);
                });

            migrationBuilder.CreateTable(
                name: "UserViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORY_ID = table.Column<int>(nullable: false),
                    BRAND_ID = table.Column<int>(nullable: false),
                    PRODUCT_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    PRODUCT_DESC = table.Column<string>(maxLength: 200, nullable: true),
                    PRODUCT_IMAGE = table.Column<string>(maxLength: 200, nullable: true),
                    PRODUCT_PRICE = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    PRODUCT_STATUS = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.PRODUCT_ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_BRAND",
                        column: x => x.BRAND_ID,
                        principalTable: "BRAND",
                        principalColumn: "BRAND_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORY",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORY",
                        principalColumn: "CATEGORY_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ORDERS_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSTOMERS_ID = table.Column<int>(nullable: false),
                    PAYMENT_ID = table.Column<int>(nullable: false),
                    ORDER_TOTAL = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    ORDER_STATUS = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ORDERS_ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_CUSTOMERS",
                        column: x => x.CUSTOMERS_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "CUSTOMERS_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDERS_PAYMENT",
                        column: x => x.PAYMENT_ID,
                        principalTable: "PAYMENT",
                        principalColumn: "PAYMENT_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Select = table.Column<bool>(nullable: false),
                    UserViewModelId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleViewModel_UserViewModel_UserViewModelId",
                        column: x => x.UserViewModelId,
                        principalTable: "UserViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_DETAIL",
                columns: table => new
                {
                    ORDER_DETAIL_ID = table.Column<int>(nullable: false),
                    ORDERS_ID = table.Column<int>(nullable: false),
                    PRODUCT_ID = table.Column<int>(nullable: false),
                    PRODUCT_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    PRODUCT_PRICE = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    PRODUCT_SALES_QUANTITY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS_DETAIL", x => new { x.PRODUCT_ID, x.ORDERS_ID, x.ORDER_DETAIL_ID });
                    table.ForeignKey(
                        name: "FK_ORDERS_DETAIL_ORDERS",
                        column: x => x.ORDERS_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ORDERS_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDERS_DETAIL_PRODUCT",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCT_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAIL_ORDERS_ID",
                table: "ORDER_DETAIL",
                column: "ORDERS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_CUSTOMERS_ID",
                table: "ORDERS",
                column: "CUSTOMERS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_PAYMENT_ID",
                table: "ORDERS",
                column: "PAYMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_BRAND_ID",
                table: "PRODUCT",
                column: "BRAND_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_CATEGORY_ID",
                table: "PRODUCT",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleViewModel_UserViewModelId",
                table: "RoleViewModel",
                column: "UserViewModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDER_DETAIL");

            migrationBuilder.DropTable(
                name: "RoleViewModel");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "UserViewModel");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "PAYMENT");

            migrationBuilder.DropTable(
                name: "BRAND");

            migrationBuilder.DropTable(
                name: "CATEGORY");
        }
    }
}
