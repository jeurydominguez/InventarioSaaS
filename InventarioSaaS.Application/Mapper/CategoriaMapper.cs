using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InventarioSaaS.Application.Mapper
{
    public class CategoriaMapper
    {
        public static CategoriaDto ACategoriaDto(Categoria model)
        {
            return new CategoriaDto
            {
                Id = model.Id,
                Nombre = model.Nombre,
                EmpresaId = model.EmpresaId,
                Descripcion = model.Descripcion
            };
        }
        public static Categoria AModelo(CategoriaDto dto)
        {
            return new Categoria
            {
                Nombre = dto.Nombre,
                EmpresaId = dto.EmpresaId,
                Descripcion = dto.Descripcion
            };
        }

        public static List<LeerCategoriaDto> ALeerCategoriaDto(List<Categoria> modelos)
        {
            List<LeerCategoriaDto> dtos = new List<LeerCategoriaDto>();

            foreach(var i in modelos)
            {
                var dto = new LeerCategoriaDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Descripcion = i.Descripcion
                };

                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
