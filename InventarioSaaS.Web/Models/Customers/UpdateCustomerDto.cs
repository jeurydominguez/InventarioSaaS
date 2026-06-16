namespace InventarioSaaS.Web.Models.Customers
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? NumeroTelefono { get; set; }

        public string? Direccion { get; set; }

        public int EmpresaId { get; set; }
    }
}
