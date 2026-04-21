using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace InventarioSaaS.Application.service
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository repository;

        public VentaService(IVentaRepository repository)
        {
            this.repository = repository;
        }

        public async Task CrearVenta(CrearVentaDto dto)
        {
            var empresa = await repository.BuscarEmpresaId();
            int empresaId = int.Parse(empresa);

            var usuario = await repository.BuscarUsuarioId();
            Guid usuarioId = Guid.Parse(usuario);

            var productos = await repository.BuscarProductos(dto.Productos, empresaId);

            ConfirmarStock(productos, dto.Productos);
            CalcularTotalPorProducto(productos, dto.Productos);
            var fecha = DateTime.UtcNow;
            var total = CalcularTotal(productos, dto.Productos);

            Venta venta = new Venta
            {
                Fecha = fecha,
                Total = total,
                TipoPago = dto.TipoPago,
                ClienteId = dto.ClienteId,
                UsuarioId = usuarioId,
                EmpresaId = empresaId
            };
            await repository.CrearVenta(venta);
            await CrearDetalle(venta, productos, dto.Productos);
            await DescontarStock(productos, dto.Productos);

        }

        public async Task<List<LeerVentasDto>> ObtenerVentas()
        {
            var empresa = await repository.BuscarEmpresaId();
            int empresaId = int.Parse(empresa);

            var ventas = await repository.GetAll(empresaId);
            var dtos = Mapper.VentasMapper.ALeerVentasDto(ventas);
            return dtos;
        }

        public async Task<LeerVentaDtoUnidad> Obtener(int id)
        {
            var empresa = await repository.BuscarEmpresaId();
            int empresaId = int.Parse(empresa);

            var venta = await repository.Obtener(id, empresaId);
            if(venta == null)
            {
                throw new NoContentEx("venta no encontrada");
            }
            var dto = Mapper.VentasMapper.ALeerVentaUnidadDto(venta);

            return dto;
        }

        public void ConfirmarStock(Dictionary<int, Producto> productoDict, List<ProductoParaVentaDto> dtos)
        {
            foreach (var dto in dtos)
            {
                if (!productoDict.TryGetValue(dto.Id, out var producto))
                    throw new Exception($"Producto con Id{dto.Id} no existe");

                if (dto.Cantidad > producto.Stock)
                    throw new Exception($"Stock Insuficiente para producto {dto.Id}");
            }
        }
        public async Task DescontarStock(Dictionary<int, Producto> productoDic, List<ProductoParaVentaDto> dtos)
        {
            foreach(var dto in dtos)
            {
                productoDic.TryGetValue(dto.Id, out var producto);
                producto.Stock -= dto.Cantidad;
            }

            await repository.ActualizarStock(productoDic);
        }

        public async Task CrearDetalle(Venta venta, Dictionary<int, Producto> productosDic, List<ProductoParaVentaDto> dtos)
        {
            List<DetalleVenta> ventas = new List<DetalleVenta>();

            foreach(var dto in dtos)
            {
                productosDic.TryGetValue(dto.Id, out var producto);

                DetalleVenta detalle = new DetalleVenta
                {
                    VentaId = venta.Id,
                    ProductoId = dto.Id,
                    Cantidad = dto.Cantidad,
                    PrecioUnitario = producto!.PrecioVenta,
                    SubTotal = dto.Total
                };
                ventas.Add(detalle);
            }
            await repository.CrearDetalle(ventas);
        }

        public decimal CalcularTotal(Dictionary<int, Producto> productosDb, List<ProductoParaVentaDto> dtos)
        {
            decimal total = 0;
            foreach (var dto in dtos)
            {
                productosDb.TryGetValue(dto.Id, out var producto);
                    
                dto.Id = producto!.Id;
                total += dto.Total;
            }
            return total;
        }

        public void CalcularTotalPorProducto(Dictionary<int, Producto> productoDict, List<ProductoParaVentaDto> dtos)
        {
            foreach(var dto in dtos)
            {
                productoDict.TryGetValue(dto.Id, out var producto);
                dto.Total = dto.Cantidad * producto!.PrecioVenta;
            }
        }
    }
}
