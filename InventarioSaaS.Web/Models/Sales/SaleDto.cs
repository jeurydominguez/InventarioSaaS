namespace InventarioSaaS.Web.Models.Sales;

using InventarioSaaS.Web.Models.Common;

public class SaleDto
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Total { get; set; }

    public TipoPago.EstadoVenta TipoPago { get; set; }

    public int? ClienteId { get; set; }

    public string? NombreCliente { get; set; }

    public string UsuarioId { get; set; }

    public string NombreVendedor { get; set; }
}