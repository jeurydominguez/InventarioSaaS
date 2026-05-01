using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IService
{
    public interface IPagoService
    {
        Task<List<LeerPagoDto>> GetAll();
        Task<LeerPagoDtoUnidad> Get(int id);
        Task CrearPago(CrearPagoDto dto);

    }
}
