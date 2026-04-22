using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColumnaVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Detalle",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_ClienteId",
                table: "Venta",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Cliente_ClienteId",
                table: "Venta",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Cliente_ClienteId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_ClienteId",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Detalle");
        }
    }
}
