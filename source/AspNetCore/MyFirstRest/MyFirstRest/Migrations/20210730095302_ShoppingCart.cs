using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstRest.Migrations
{
    public partial class ShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ArticleNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartPositions_Articles_ArticleNumber",
                        column: x => x.ArticleNumber,
                        principalTable: "Articles",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartPositions_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartPositions_ArticleNumber",
                table: "ShoppingCartPositions",
                column: "ArticleNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartPositions_ShoppingCartId",
                table: "ShoppingCartPositions",
                column: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartPositions");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
