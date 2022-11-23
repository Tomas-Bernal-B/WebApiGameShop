using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIGameShop.Migrations
{
    public partial class Opinions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "VideoGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Opinions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoGameID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Opinions_VideoGames_VideoGameID",
                        column: x => x.VideoGameID,
                        principalTable: "VideoGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_VideoGameID",
                table: "Opinions",
                column: "VideoGameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opinions");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "VideoGames",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
