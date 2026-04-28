using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required decimal PrecioVenta { get; set; }

        [Required]
        public required int Stock { get; set; }

        public int? EmpresaId { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        [Unicode(false)]
        public string? Foto { get; set; }
    }
}
