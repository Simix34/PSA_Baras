using Microsoft.EntityFrameworkCore.Migrations;

namespace PSA_Baras.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktail_CartItem_cart_itemId",
                table: "Cocktail");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_authorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Cocktail_cocktailId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Cocktail_cocktailId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_cocktailId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Cocktail_cart_itemId",
                table: "Cocktail");

            migrationBuilder.DropColumn(
                name: "cocktailId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "cart_itemId",
                table: "Cocktail");

            migrationBuilder.AddColumn<string>(
                name: "units",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cocktailId",
                table: "CartItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CocktailProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unit = table.Column<string>(nullable: true),
                    quantity = table.Column<double>(nullable: false),
                    cocktailId = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CocktailProduct_Cocktail_cocktailId",
                        column: x => x.cocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailProduct_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_cocktailId",
                table: "CartItem",
                column: "cocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailProduct_cocktailId",
                table: "CocktailProduct",
                column: "cocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailProduct_productId",
                table: "CocktailProduct",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cocktail_cocktailId",
                table: "CartItem",
                column: "cocktailId",
                principalTable: "Cocktail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_authorId",
                table: "Comment",
                column: "authorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Cocktail_cocktailId",
                table: "Comment",
                column: "cocktailId",
                principalTable: "Cocktail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cocktail_cocktailId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_authorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Cocktail_cocktailId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "CocktailProduct");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_cocktailId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "units",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "cocktailId",
                table: "CartItem");

            migrationBuilder.AddColumn<int>(
                name: "cocktailId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cart_itemId",
                table: "Cocktail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_cocktailId",
                table: "Product",
                column: "cocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_Cocktail_cart_itemId",
                table: "Cocktail",
                column: "cart_itemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktail_CartItem_cart_itemId",
                table: "Cocktail",
                column: "cart_itemId",
                principalTable: "CartItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_authorId",
                table: "Comment",
                column: "authorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Cocktail_cocktailId",
                table: "Comment",
                column: "cocktailId",
                principalTable: "Cocktail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Cocktail_cocktailId",
                table: "Product",
                column: "cocktailId",
                principalTable: "Cocktail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
