using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class agregandoNavDeUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId1",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_UsuarioId1",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Venta");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Venta",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_UsuarioId",
                table: "Venta",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId",
                table: "Venta",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_UsuarioId",
                table: "Venta");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Venta",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Venta",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_UsuarioId1",
                table: "Venta",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId1",
                table: "Venta",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
