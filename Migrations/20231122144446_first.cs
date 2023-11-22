using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nooshop.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmartPhone",
                table: "SmartPhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shop",
                table: "Shop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sell",
                table: "Sell");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laptop",
                table: "Laptop");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "SmartPhone",
                newName: "SmartPhones");

            migrationBuilder.RenameTable(
                name: "Shop",
                newName: "Shops");

            migrationBuilder.RenameTable(
                name: "Sell",
                newName: "Sells");

            migrationBuilder.RenameTable(
                name: "Laptop",
                newName: "Laptops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmartPhones",
                table: "SmartPhones",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shops",
                table: "Shops",
                column: "SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sells",
                table: "Sells",
                column: "sellId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmartPhones",
                table: "SmartPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shops",
                table: "Shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sells",
                table: "Sells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "SmartPhones",
                newName: "SmartPhone");

            migrationBuilder.RenameTable(
                name: "Shops",
                newName: "Shop");

            migrationBuilder.RenameTable(
                name: "Sells",
                newName: "Sell");

            migrationBuilder.RenameTable(
                name: "Laptops",
                newName: "Laptop");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmartPhone",
                table: "SmartPhone",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shop",
                table: "Shop",
                column: "SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sell",
                table: "Sell",
                column: "sellId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laptop",
                table: "Laptop",
                column: "ProductID");
        }
    }
}
