using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Application.Mapper
{
    public class PagoMapper
    {
        public static List<LeerPagoDto> ALeerPagoDto(List<Pago> modelos)
        {
            List<LeerPagoDto> dtos = new List<LeerPagoDto>();
            foreach (var i in modelos)
            {
                var dto = new LeerPagoDto
                {
                    Id = i.Id,
                    CuentasPorCobrarId = i.CuentasPorCobrarId,
                    Monto = i.Monto,
                    Fecha = i.Fecha
                };
                dtos.Add(dto);
            }
            return dtos;
        }
        public static LeerPagoDtoUnidad ALeerPagoDtoUnidad(Pago modelo)
        {
            return new LeerPagoDtoUnidad
            {
                Id = modelo.Id,
                CuentasPorCobrarId = modelo.CuentasPorCobrarId,
                CuentaPorCobrar = modelo.CuentaPorCobrar,
                Fecha = modelo.Fecha,
                Monto = modelo.Monto
            };
        }
        public static Pago AModeloPago(CrearPagoDto dto)
        {
            return new Pago
            {
                CuentasPorCobrarId = dto.CuentaPorCobrarId,
                Monto = dto.Monto,
                Fecha = DateTime.UtcNow,
                EmpresaId = dto.EmpresaId
            };

        }
    }
}
