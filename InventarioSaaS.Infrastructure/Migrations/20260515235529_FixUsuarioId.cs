using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Venta",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_UsuarioId1",
                table: "Venta",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_ProductoId",
                table: "Detalle",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_Producto_ProductoId",
                table: "Detalle",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId1",
                table: "Venta",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_Producto_ProductoId",
                table: "Detalle");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId1",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_UsuarioId1",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_ProductoId",
                table: "Detalle");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Venta");
        }
    }
}
