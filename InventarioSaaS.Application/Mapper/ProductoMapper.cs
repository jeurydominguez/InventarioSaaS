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

        public static List<LeerProductoDto> AListaDto(List<Producto> productos)
        {
            List<LeerProductoDto> productosDtos = new List<LeerProductoDto>();

            foreach (var i in productos)
            {
                var productoDto = new LeerProductoDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    PrecioVenta = i.PrecioVenta,
                    Stock = i.Stock
                };
                productosDtos.Add(productoDto);
            }

            return productosDtos;
        }
    }
}
