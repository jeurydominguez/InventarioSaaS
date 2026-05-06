using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Application.Mapper
{
    public class ReportesMaper
    {
        public static List<VentasPorDiaDto> AVentasPorDiaDto(List<Venta> models)
        {
            List<VentasPorDiaDto> dtos = new List<VentasPorDiaDto>();

            foreach (var i in models)
            {
                var detalle = Mapper.DetalleVentaMapper.ADetalleVentaDto(i.Detalles);
                var cuentas = i.CuentaPorCobrar != null
                    ? Mapper.ReportesMaper.ALeerCuentasPorCobrarReportes(i.CuentaPorCobrar) : null;
                var cliente = i.cliente != null
                    ? Mapper.ClienteMapper.ALeerClienteDtoVenta(i.cliente) : null;
                var dto = new VentasPorDiaDto
                {
                    Id = i.Id,
                    Total = i.Total,
                    Detalles = detalle,
                    TipoPago = i.TipoPago,
                    Cliente = cliente,
                    CuentaPorCobrar = cuentas,
                    UsuarioId = i.UsuarioId
                };
                dtos.Add(dto);
            }
            return dtos;
        }

        public static LeerCuentasPorCobrarReportes ALeerCuentasPorCobrarReportes(CuentasPorCobrar model)
        {
            return new LeerCuentasPorCobrarReportes
            {
                Id = model.Id,
                VentaID = model.VentaId,
                MontoTotal = model.MontoTotal,
                MontoPendiente = model.MontoPendiente,
                Estado = model.Estado
            };
        }

        public static List<ClientesCondeudaDto> AClientesConDeuda(List<Cliente> modelo)
        {
            List<ClientesCondeudaDto> dtos = new List<ClientesCondeudaDto>();

            foreach(var i in modelo)
            {
                var deudas = Mapper.ReportesMaper.AListaCuentasPorCobrarReportes(i.Deudas);
                var dto = new ClientesCondeudaDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    NumeroTelefono = i.NumeroTelefono,
                    Deudas = deudas,
                    Direccion = i.Direccion
                };
                dtos.Add(dto);
            }
            return dtos;
        }
        public static List<LeerCuentasPorCobrarReportes> AListaCuentasPorCobrarReportes(List<CuentasPorCobrar> model)
        {
            List<LeerCuentasPorCobrarReportes> dtos = new List<LeerCuentasPorCobrarReportes>();
            foreach(var i in model)
            {
                var dto = new LeerCuentasPorCobrarReportes
                {
                    Id = i.Id,
                    VentaID = i.VentaId,
                    MontoTotal = i.MontoTotal,
                    MontoPendiente = i.MontoPendiente,
                    Estado = i.Estado
                };
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
