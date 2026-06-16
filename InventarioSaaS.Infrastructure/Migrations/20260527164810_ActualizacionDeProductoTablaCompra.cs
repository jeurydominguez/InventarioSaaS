using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionDeProductoTablaCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCompra",
                table: "Producto",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCompra",
                table: "Detalle",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVenta",
                table: "Detalle",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioCompra",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "PrecioCompra",
                table: "Detalle");

            migrationBuilder.DropColumn(
                name: "PrecioVenta",
                table: "Detalle");
        }
    }
}
