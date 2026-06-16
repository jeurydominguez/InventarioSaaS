using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustePrecioVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioVenta",
                table: "Detalle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVenta",
                table: "Detalle",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
