using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Database.Migrations
{
    /// <inheritdoc />
    public partial class MigracionMecanicoOrden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    NumeroOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Servicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlacaVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MecanicoAsignadoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.NumeroOrden);
                    table.ForeignKey(
                        name: "FK_Ordenes_AspNetUsers_MecanicoAsignadoId",
                        column: x => x.MecanicoAsignadoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_MecanicoAsignadoId",
                table: "Ordenes",
                column: "MecanicoAsignadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EstaDisponible",
                table: "AspNetUsers");

        }
    }
}
