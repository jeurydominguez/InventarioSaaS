using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace InventarioSaaS.Domain.IRepository
{
    public interface IVentaRepository
    {
        Task<string> BuscarEmpresaId();
        Task<string> BuscarUsuarioId();
        Task<Dictionary<int, Producto>> BuscarProductos(List<ProductoParaVentaDto> productosDto, int empresaId);
        Task CrearVenta(Venta venta);
        Task CrearDetalle(List<DetalleVenta> detalles);
        Task ActualizarStock(Dictionary<int, Producto> produtos);
        Task<List<Venta>> GetAll(int empresaId);
        Task<Venta> Obtener(int id, int empresaId);
    }
}
