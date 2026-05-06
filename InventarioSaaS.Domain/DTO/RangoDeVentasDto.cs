using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class RangoDeVentasDto
    {
        [Required]
        public DateTime Inicial { get; set; }

        [Required]
        public DateTime Final { get; set; }
    }
}
