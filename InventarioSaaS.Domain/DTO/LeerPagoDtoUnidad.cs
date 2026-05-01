using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerPagoDtoUnidad
    {
        public int Id { get; set; }
        public CuentasPorCobrar CuentaPorCobrar { get; set; }
        public int CuentasPorCobrarId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
