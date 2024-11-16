using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Database.Migrations
{
    /// <inheritdoc />
    public partial class MigracionCambioMecanico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mecanicos_AspNetUsers_UsuarioId1",
                table: "Mecanicos");

            migrationBuilder.DropIndex(
                name: "IX_Mecanicos_UsuarioId1",
                table: "Mecanicos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Mecanicos");

            migrationBuilder.AddForeignKey(
                name: "FK_Mecanicos_AspNetUsers_UsuarioId",
                table: "Mecanicos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mecanicos_AspNetUsers_UsuarioId",
                table: "Mecanicos");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Mecanicos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mecanicos_UsuarioId1",
                table: "Mecanicos",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Mecanicos_AspNetUsers_UsuarioId1",
                table: "Mecanicos",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
