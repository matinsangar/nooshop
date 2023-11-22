using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nooshop.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shops",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Shops");

            migrationBuilder.AddColumn<string>(
                name: "sellerCode",
                table: "Shops",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shops",
                table: "Shops",
                column: "sellerCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shops",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "sellerCode",
                table: "Shops");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shops",
                table: "Shops",
                column: "SellerId");
        }
    }
}
