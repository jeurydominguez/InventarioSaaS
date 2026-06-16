using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class CrearUsuarioDto
    {
        [Required]
        public required string rol { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public required string NombreUsuario { get; set; }

        [Required]
        public string Apellido { get; set; }
    }
}
