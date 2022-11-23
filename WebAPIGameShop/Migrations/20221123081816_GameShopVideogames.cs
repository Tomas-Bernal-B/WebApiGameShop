using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIGameShop.Migrations
{
    public partial class GameShopVideogames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameShopVideogames",
                columns: table => new
                {
                    VideogameId = table.Column<int>(type: "int", nullable: false),
                    GameShopId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameShopVideogames", x => new { x.GameShopId, x.VideogameId });
                    table.ForeignKey(
                        name: "FK_GameShopVideogames_Games_GameShopId",
                        column: x => x.GameShopId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameShopVideogames_VideoGames_VideogameId",
                        column: x => x.VideogameId,
                        principalTable: "VideoGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameShopVideogames_VideogameId",
                table: "GameShopVideogames",
                column: "VideogameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameShopVideogames");
        }
    }
}
