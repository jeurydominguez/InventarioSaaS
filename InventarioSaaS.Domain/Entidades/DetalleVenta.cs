using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.Entidades
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int VentaId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public Venta Venta { get; set; }

        public decimal SubTotal { get; set; }
    }
}
