using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.Entidades
{
    public class CategoriaQuery : QueryParameters
    {
        public string? Search { get; set; }

        public string? Descripcion { get; set; }

    }
}
