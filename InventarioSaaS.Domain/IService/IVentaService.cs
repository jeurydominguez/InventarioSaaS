using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IVentaService
    {
        Task CrearVenta(CrearVentaDto dto);
        Task<List<LeerVentasDto>> ObtenerVentas();
        Task<LeerVentaDtoUnidad> Obtener(int id);
        Task<PagedResponse<LeerVentasDto>> ObtenerP(VentasQuery query);
    }
}
