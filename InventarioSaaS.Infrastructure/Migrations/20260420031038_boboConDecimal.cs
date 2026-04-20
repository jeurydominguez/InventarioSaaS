using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class boboConDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                table: "Detalle",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_VentaId",
                table: "Detalle",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_Venta_VentaId",
                table: "Detalle",
                column: "VentaId",
                principalTable: "Venta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_Venta_VentaId",
                table: "Detalle");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_VentaId",
                table: "Detalle");

            migrationBuilder.AlterColumn<float>(
                name: "PrecioUnitario",
                table: "Detalle",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
