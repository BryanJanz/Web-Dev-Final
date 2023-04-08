using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _245Final.Data.Migrations
{
    public partial class added_cart_things : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartUserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartUserID);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GameID1 = table.Column<int>(type: "int", nullable: false),
                    CartUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartUserID",
                        column: x => x.CartUserID,
                        principalTable: "Cart",
                        principalColumn: "CartUserID");
                    table.ForeignKey(
                        name: "FK_CartItem_Games_GameID1",
                        column: x => x.GameID1,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartUserID",
                table: "CartItem",
                column: "CartUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_GameID1",
                table: "CartItem",
                column: "GameID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
