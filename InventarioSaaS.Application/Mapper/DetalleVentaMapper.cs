using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Application.Mapper
{
    public class DetalleVentaMapper
    {
        public static List<LeerDetalleVentaDto> ADetalleVentaDto(List<DetalleVenta> detalleVenta)
        {
            List<LeerDetalleVentaDto> dtos = new List<LeerDetalleVentaDto>();

            foreach(var modelo in detalleVenta)
            {
                LeerDetalleVentaDto dto = new LeerDetalleVentaDto
                {
                    Id = modelo.Id,
                    VentaId = modelo.VentaId,
                    Nombre = modelo.Nombre,
                    ProductoId = modelo.ProductoId,
                    Cantidad = modelo.Cantidad,
                    PrecioUnitario = modelo.PrecioUnitario,
                    SubTotal = modelo.SubTotal
                };

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
