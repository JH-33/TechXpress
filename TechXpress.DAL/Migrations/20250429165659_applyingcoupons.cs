using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechXpress.DAL.Migrations
{
    /// <inheritdoc />
    public partial class applyingcoupons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "catgoryid",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_categoryid");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageLimit = table.Column<int>(type: "int", nullable: true),
                    UsageCount = table.Column<int>(type: "int", nullable: true),
                    couponDiscountinPercentage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CouponCode",
                table: "ShoppingCarts",
                column: "CouponCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryid",
                table: "Products",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Coupons_CouponCode",
                table: "ShoppingCarts",
                column: "CouponCode",
                principalTable: "Coupons",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Coupons_CouponCode",
                table: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_CouponCode",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "categoryid",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_categoryid",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "catgoryid",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
