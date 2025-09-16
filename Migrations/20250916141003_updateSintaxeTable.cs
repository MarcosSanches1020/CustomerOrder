using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrders.API.Migrations
{
    /// <inheritdoc />
    public partial class updateSintaxeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_carts_cartId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_products_productId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_customers_CustomerId",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carts",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "PRODUCTS");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "CUSTOMER");

            migrationBuilder.RenameTable(
                name: "carts",
                newName: "CART");

            migrationBuilder.RenameTable(
                name: "cartItems",
                newName: "CART_ITEM");

            migrationBuilder.RenameColumn(
                name: "PriceProduct",
                table: "PRODUCTS",
                newName: "PRICE_PRODUCT");

            migrationBuilder.RenameColumn(
                name: "NameProduct",
                table: "PRODUCTS",
                newName: "NAME_PRODUCT");

            migrationBuilder.RenameColumn(
                name: "tipoDoProduto",
                table: "PRODUCTS",
                newName: "TYPE_PRODUCT");

            migrationBuilder.RenameColumn(
                name: "Descrição",
                table: "PRODUCTS",
                newName: "DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "idProduct",
                table: "PRODUCTS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "CUSTOMER",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CUSTOMER",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "idCustomer",
                table: "CUSTOMER",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CART",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CART",
                newName: "CUSTOMER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_carts_CustomerId",
                table: "CART",
                newName: "IX_CART_CUSTOMER_ID");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "CART_ITEM",
                newName: "QUANTITY");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "CART_ITEM",
                newName: "PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "cartId",
                table: "CART_ITEM",
                newName: "CART_ID");

            migrationBuilder.RenameColumn(
                name: "idCartItems",
                table: "CART_ITEM",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_productId",
                table: "CART_ITEM",
                newName: "IX_CART_ITEM_PRODUCT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_cartId",
                table: "CART_ITEM",
                newName: "IX_CART_ITEM_CART_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCTS",
                table: "PRODUCTS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CUSTOMER",
                table: "CUSTOMER",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CART",
                table: "CART",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CART_ITEM",
                table: "CART_ITEM",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CART_CUSTOMER_CUSTOMER_ID",
                table: "CART",
                column: "CUSTOMER_ID",
                principalTable: "CUSTOMER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CART_ITEM_CART_CART_ID",
                table: "CART_ITEM",
                column: "CART_ID",
                principalTable: "CART",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CART_ITEM_PRODUCTS_PRODUCT_ID",
                table: "CART_ITEM",
                column: "PRODUCT_ID",
                principalTable: "PRODUCTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CART_CUSTOMER_CUSTOMER_ID",
                table: "CART");

            migrationBuilder.DropForeignKey(
                name: "FK_CART_ITEM_CART_CART_ID",
                table: "CART_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_CART_ITEM_PRODUCTS_PRODUCT_ID",
                table: "CART_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCTS",
                table: "PRODUCTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CUSTOMER",
                table: "CUSTOMER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CART_ITEM",
                table: "CART_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CART",
                table: "CART");

            migrationBuilder.RenameTable(
                name: "PRODUCTS",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "CUSTOMER",
                newName: "customers");

            migrationBuilder.RenameTable(
                name: "CART_ITEM",
                newName: "cartItems");

            migrationBuilder.RenameTable(
                name: "CART",
                newName: "carts");

            migrationBuilder.RenameColumn(
                name: "PRICE_PRODUCT",
                table: "products",
                newName: "PriceProduct");

            migrationBuilder.RenameColumn(
                name: "NAME_PRODUCT",
                table: "products",
                newName: "NameProduct");

            migrationBuilder.RenameColumn(
                name: "TYPE_PRODUCT",
                table: "products",
                newName: "tipoDoProduto");

            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                table: "products",
                newName: "Descrição");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "products",
                newName: "idProduct");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "customers",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "customers",
                newName: "idCustomer");

            migrationBuilder.RenameColumn(
                name: "QUANTITY",
                table: "cartItems",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "PRODUCT_ID",
                table: "cartItems",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "CART_ID",
                table: "cartItems",
                newName: "cartId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "cartItems",
                newName: "idCartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CART_ITEM_PRODUCT_ID",
                table: "cartItems",
                newName: "IX_cartItems_productId");

            migrationBuilder.RenameIndex(
                name: "IX_CART_ITEM_CART_ID",
                table: "cartItems",
                newName: "IX_cartItems_cartId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "carts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CUSTOMER_ID",
                table: "carts",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CART_CUSTOMER_ID",
                table: "carts",
                newName: "IX_carts_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "idProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "idCustomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems",
                column: "idCartItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carts",
                table: "carts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_carts_cartId",
                table: "cartItems",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_products_productId",
                table: "cartItems",
                column: "productId",
                principalTable: "products",
                principalColumn: "idProduct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_customers_CustomerId",
                table: "carts",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "idCustomer",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
