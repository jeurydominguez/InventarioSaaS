using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerProductoDtoUnidad
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }
        public int EmpresaId { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaDto Categoria { get; set;}
    }
}
