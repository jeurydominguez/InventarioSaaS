using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Application.Mapper
{
    public class ProductoMapper
    {
        public static Producto AModelo(CrearProductoDto dto, int empresa)
        {
            return new Producto
            {
                Nombre = dto.Nombre,
                PrecioVenta = dto.PrecioVenta,
                Stock = dto.Stock,
                EmpresaId = empresa
            };
        }
    }
}
