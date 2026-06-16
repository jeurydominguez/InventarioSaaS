using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.Entidades
{
    public class VentasQuery : QueryParameters
    {
        public DateOnly? Fecha { get; set; }

        public string? QuerySeach { get; set; }
    }
}
