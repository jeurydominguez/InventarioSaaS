using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }
        public int EmpresaId { get; set; }
        public required string Descripcion { get; set; }

    }
}
