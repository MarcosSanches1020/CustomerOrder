using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrders.API.Migrations
{
    /// <inheritdoc />
    public partial class updatPriceProductToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceProduct",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PriceProduct",
                table: "products",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
