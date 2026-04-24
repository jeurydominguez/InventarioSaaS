using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Application.Mapper
{
    public class CuentasPorCobrarMapper
    {
        public static List<LeerCuentasPorCobrarDto> ALeerCuentasPorCobrar(List<CuentasPorCobrar> modelos)
        {
            List<LeerCuentasPorCobrarDto> dtos = new List<LeerCuentasPorCobrarDto>();

            foreach(var i in modelos)
            {
                var dto = new LeerCuentasPorCobrarDto
                {
                    Id = i.Id,
                    VentaId = i.VentaId,
                    NombreCliente = i.Cliente.Nombre,
                    MontoTotal = i.MontoTotal,
                    MontoPendiente = i.MontoPendiente,
                    Estado = i.Estado,
                    FechaCreacion = i.FechaCreacion
                };
                dtos.Add(dto);
            }
            return dtos;
        }

        public static LeerCuentaPorCobrarUnidadDto ALeerCuentasPorCobrarUnidad(CuentasPorCobrar modelo)
        {
            var venta = Mapper.VentasMapper.AleerVentaDtoCuenta(modelo.Venta);
            var cliente = Mapper.ClienteMapper.ALeerClienteDtoVenta(modelo.Cliente);
            return new LeerCuentaPorCobrarUnidadDto
            {
                Id = modelo.Id,
                Venta = venta,
                Cliente = cliente,
                MontoTotal = modelo.MontoTotal,
                MontoPendiente = modelo.MontoPendiente,
                Estado = modelo.Estado,
                FechaCreacion = modelo.FechaCreacion
            };
        }
    }
}
