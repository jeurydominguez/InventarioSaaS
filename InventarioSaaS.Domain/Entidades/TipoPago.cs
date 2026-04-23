using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.Entidades
{
    public class TipoPago
    {
        public enum EstadoVenta
        {
            Contado = 0,
            credito = 1
        }
    }
}
