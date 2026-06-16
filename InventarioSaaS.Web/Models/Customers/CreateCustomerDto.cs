using System.ComponentModel.DataAnnotations;

namespace InventarioSaaS.Web.Models.Customers
{
    public class CreateCustomerDto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = "";

        public string? NumeroTelefono { get; set; }

        public string? Direccion { get; set; }

        public int EmpresaId { get; set; }
    }
}
