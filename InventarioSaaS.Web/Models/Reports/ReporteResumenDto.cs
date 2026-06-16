namespace InventarioSaaS.Web.Models.Reports
{
    public class ReporteResumenDto
    {
        public decimal Ingresos { get; set; }

        public int Facturas { get; set; }

        public int Clientes { get; set; }

        public decimal Gastos { get; set; }

        public decimal Conversion { get; set; }
    }
}
