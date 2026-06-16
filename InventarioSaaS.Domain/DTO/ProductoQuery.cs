using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class ProductoQuery : QueryParameters
    {
        public string? Search { get; set; }

        public string? Categoria { get; set; }

        public bool? Activo { get; set; }
    }
}
