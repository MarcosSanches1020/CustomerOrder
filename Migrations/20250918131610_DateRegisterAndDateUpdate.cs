using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrders.API.Migrations
{
    /// <inheritdoc />
    public partial class DateRegisterAndDateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_REGISTER",
                table: "PRODUCTS",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_UPDATE",
                table: "PRODUCTS",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_REGISTER",
                table: "CUSTOMER",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_UPDATE",
                table: "CUSTOMER",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_REGISTER",
                table: "CART_ITEM",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_UPDATE",
                table: "CART_ITEM",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_REGISTER",
                table: "CART",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_UPDATE",
                table: "CART",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA_REGISTER",
                table: "PRODUCTS");

            migrationBuilder.DropColumn(
                name: "DATA_UPDATE",
                table: "PRODUCTS");

            migrationBuilder.DropColumn(
                name: "DATA_REGISTER",
                table: "CUSTOMER");

            migrationBuilder.DropColumn(
                name: "DATA_UPDATE",
                table: "CUSTOMER");

            migrationBuilder.DropColumn(
                name: "DATA_REGISTER",
                table: "CART_ITEM");

            migrationBuilder.DropColumn(
                name: "DATA_UPDATE",
                table: "CART_ITEM");

            migrationBuilder.DropColumn(
                name: "DATA_REGISTER",
                table: "CART");

            migrationBuilder.DropColumn(
                name: "DATA_UPDATE",
                table: "CART");
        }
    }
}
