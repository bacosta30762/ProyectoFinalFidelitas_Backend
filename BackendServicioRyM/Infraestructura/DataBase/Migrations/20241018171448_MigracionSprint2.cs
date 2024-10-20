using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Database.Migrations
{
    /// <inheritdoc />
    public partial class MigracionSprint2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_AspNetUsers_MecanicoAsignadoId",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Acciones",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Servicio",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EstaDisponible",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "MecanicoAsignadoId",
                table: "Ordenes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteId",
                table: "Ordenes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Dia",
                table: "Ordenes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Hora",
                table: "Ordenes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicioId",
                table: "Ordenes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Mecanicos",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsuarioId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mecanicos", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Mecanicos_AspNetUsers_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MecanicoServicio",
                columns: table => new
                {
                    MecanicosUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiciosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MecanicoServicio", x => new { x.MecanicosUsuarioId, x.ServiciosId });
                    table.ForeignKey(
                        name: "FK_MecanicoServicio_Mecanicos_MecanicosUsuarioId",
                        column: x => x.MecanicosUsuarioId,
                        principalTable: "Mecanicos",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MecanicoServicio_Servicios_ServiciosId",
                        column: x => x.ServiciosId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ClienteId",
                table: "Ordenes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ServicioId",
                table: "Ordenes",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mecanicos_UsuarioId1",
                table: "Mecanicos",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_MecanicoServicio_ServiciosId",
                table: "MecanicoServicio",
                column: "ServiciosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_AspNetUsers_ClienteId",
                table: "Ordenes",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoAsignadoId",
                table: "Ordenes",
                column: "MecanicoAsignadoId",
                principalTable: "Mecanicos",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Servicios_ServicioId",
                table: "Ordenes",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_AspNetUsers_ClienteId",
                table: "Ordenes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Mecanicos_MecanicoAsignadoId",
                table: "Ordenes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Servicios_ServicioId",
                table: "Ordenes");

            migrationBuilder.DropTable(
                name: "MecanicoServicio");

            migrationBuilder.DropTable(
                name: "Mecanicos");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_ClienteId",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_ServicioId",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Dia",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "Ordenes");

            migrationBuilder.AlterColumn<string>(
                name: "MecanicoAsignadoId",
                table: "Ordenes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Acciones",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Servicio",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Especialidad",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstaDisponible",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_AspNetUsers_MecanicoAsignadoId",
                table: "Ordenes",
                column: "MecanicoAsignadoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
