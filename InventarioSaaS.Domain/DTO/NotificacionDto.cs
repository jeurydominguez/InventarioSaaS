using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class NotificacionDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = "";

        public string Mensaje { get; set; } = "";

        public string Tipo { get; set; } = "";

        public DateTime Fecha { get; set; }

        public bool Leida { get; set; }
    }
    public static class TipoNotificacion
    {
        public const string StockBajo = "stock";
        public const string Venta = "venta";
        public const string Cliente = "cliente";
        public const string Factura = "factura";
    }
}
