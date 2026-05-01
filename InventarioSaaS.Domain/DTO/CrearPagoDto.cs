using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class CrearPagoDto
    {
        public int Id { get; set; }

        [Required]
        public required int CuentaPorCobrarId { get; set; }

        [Required]
        public required decimal Monto { get; set; }

        public int? EmpresaId { get; set; }
    }
}
