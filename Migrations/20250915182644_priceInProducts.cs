using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrders.API.Migrations
{
    /// <inheritdoc />
    public partial class priceInProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceProduct",
                table: "products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceProduct",
                table: "products");
        }
    }
}
