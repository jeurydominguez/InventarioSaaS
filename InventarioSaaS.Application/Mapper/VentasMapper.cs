using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Application.Mapper
{
    public class VentasMapper
    {
        public static List<LeerVentasDto> ALeerVentasDto(List<Venta> modelos)
        {
            List<LeerVentasDto> dtos = new List<LeerVentasDto>();

            foreach(var modelo in modelos)
            {
                LeerVentasDto dto = new LeerVentasDto
                {
                    Id = modelo.Id,
                    Fecha = modelo.Fecha,
                    Total = modelo.Total,
                    TipoPago = modelo.TipoPago,
                    clienteId = modelo.ClienteId,
                    NombreCliente = modelo.cliente != null ? modelo.cliente.Nombre : "Cliente General",
                    UsuarioId = modelo.UsuarioId,
                    NombreVendedor = modelo.Usuario != null
            ? modelo.Usuario.NombreCompleto
            : "Sin vendedor",
                };
                dtos.Add(dto);
            }
            return dtos;
        }
        public static LeerVentasDto AleerVentaDtoCuenta(Venta modelo)
        {
            return new LeerVentasDto
            {
                Id = modelo.Id,
                Fecha = modelo.Fecha,
                Total = modelo.Total,
                TipoPago = modelo.TipoPago,
                clienteId = modelo.ClienteId
            };
        }

        public static LeerVentaDtoUnidad ALeerVentaUnidadDto(Venta modelo)
        {
            var detalles = Mapper.DetalleVentaMapper.ADetalleVentaDto(modelo.Detalles);
            var cliente = Mapper.ClienteMapper.ALeerClienteDtoVenta(modelo.cliente);

            return new LeerVentaDtoUnidad
            {
                Id = modelo.Id,
                Fecha = modelo.Fecha,
                Total = modelo.Total,
                TipoPago = modelo.TipoPago,
                clienteId = modelo.ClienteId,
                Cliente = cliente,
                UsuarioId = modelo.UsuarioId,
                detalle = detalles
            };
        }
    }
}
