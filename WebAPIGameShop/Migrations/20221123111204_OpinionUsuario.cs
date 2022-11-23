using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIGameShop.Migrations
{
    public partial class OpinionUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Opinions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_UsuarioId",
                table: "Opinions",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinions_AspNetUsers_UsuarioId",
                table: "Opinions",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_AspNetUsers_UsuarioId",
                table: "Opinions");

            migrationBuilder.DropIndex(
                name: "IX_Opinions_UsuarioId",
                table: "Opinions");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Opinions");
        }
    }
}
