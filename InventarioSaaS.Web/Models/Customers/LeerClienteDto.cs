using InventarioSaaS.Web.Models.Sales;

namespace InventarioSaaS.Web.Models.Customers
{
    public class LeerClienteDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";

        public string? NumeroTelefono { get; set; }

        public string? Direccion { get; set; }

        public List<SaleDto> Facturas { get; set; } = [];
    }
}
