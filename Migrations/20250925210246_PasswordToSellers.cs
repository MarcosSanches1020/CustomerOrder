using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrders.API.Migrations
{
    /// <inheritdoc />
    public partial class PasswordToSellers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PASSWORD",
                table: "SELLERS",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PASSWORD",
                table: "SELLERS");
        }
    }
}
