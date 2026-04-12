using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class RegistrarUsuarioDTO
    {
        [Required]
        public required string NombreEmpresa { get; set; }

        public string? rol { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        [EmailAddress]
        public required string EmpresaEmail { get; set; }

        [Required]
        public required string NombreUsuario { get; set; }

        [Required]
        public string Apellido { get; set; }

        public int EmpresaId { get; set; }
    }
}
