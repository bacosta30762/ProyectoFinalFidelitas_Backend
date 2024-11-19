using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Database.Migrations
{
    /// <inheritdoc />
    public partial class ArregloRelacionMecanicoServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MecanicoServicio_Mecanicos_MecanicosUsuarioId",
                table: "MecanicoServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_MecanicoServicio_Servicios_ServiciosId",
                table: "MecanicoServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoAsignadoId",
                table: "Ordenes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MecanicoServicio",
                table: "MecanicoServicio");

            migrationBuilder.DropIndex(
                name: "IX_MecanicoServicio_ServiciosId",
                table: "MecanicoServicio");

            migrationBuilder.RenameColumn(
                name: "ServiciosId",
                table: "MecanicoServicio",
                newName: "ServicioId");

            migrationBuilder.RenameColumn(
                name: "MecanicosUsuarioId",
                table: "MecanicoServicio",
                newName: "UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "MecanicoAsignadoId",
                table: "Ordenes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "MecanicoUsuarioId",
                table: "Ordenes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MecanicoServicio",
                table: "MecanicoServicio",
                columns: new[] { "ServicioId", "UsuarioId" });

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_MecanicoUsuarioId",
                table: "Ordenes",
                column: "MecanicoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MecanicoServicio_UsuarioId",
                table: "MecanicoServicio",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_MecanicoServicio_Mecanicos_UsuarioId",
                table: "MecanicoServicio",
                column: "UsuarioId",
                principalTable: "Mecanicos",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MecanicoServicio_Servicios_ServicioId",
                table: "MecanicoServicio",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoAsignadoId",
                table: "Ordenes",
                column: "MecanicoAsignadoId",
                principalTable: "Mecanicos",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoUsuarioId",
                table: "Ordenes",
                column: "MecanicoUsuarioId",
                principalTable: "Mecanicos",
                principalColumn: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MecanicoServicio_Mecanicos_UsuarioId",
                table: "MecanicoServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_MecanicoServicio_Servicios_ServicioId",
                table: "MecanicoServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoAsignadoId",
                table: "Ordenes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoUsuarioId",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_MecanicoUsuarioId",
                table: "Ordenes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MecanicoServicio",
                table: "MecanicoServicio");

            migrationBuilder.DropIndex(
                name: "IX_MecanicoServicio_UsuarioId",
                table: "MecanicoServicio");

            migrationBuilder.DropColumn(
                name: "MecanicoUsuarioId",
                table: "Ordenes");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "MecanicoServicio",
                newName: "MecanicosUsuarioId");

            migrationBuilder.RenameColumn(
                name: "ServicioId",
                table: "MecanicoServicio",
                newName: "ServiciosId");

            migrationBuilder.AlterColumn<string>(
                name: "MecanicoAsignadoId",
                table: "Ordenes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MecanicoServicio",
                table: "MecanicoServicio",
                columns: new[] { "MecanicosUsuarioId", "ServiciosId" });

            migrationBuilder.CreateIndex(
                name: "IX_MecanicoServicio_ServiciosId",
                table: "MecanicoServicio",
                column: "ServiciosId");

            migrationBuilder.AddForeignKey(
                name: "FK_MecanicoServicio_Mecanicos_MecanicosUsuarioId",
                table: "MecanicoServicio",
                column: "MecanicosUsuarioId",
                principalTable: "Mecanicos",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MecanicoServicio_Servicios_ServiciosId",
                table: "MecanicoServicio",
                column: "ServiciosId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoAsignadoId",
                table: "Ordenes",
                column: "MecanicoAsignadoId",
                principalTable: "Mecanicos",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
