using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Application.service
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            this.repository = repository;
        }

        public async Task Crear(CategoriaDto dto)
        {
            var empresa = await repository.BuscarEmpresa();
            if (empresa == null)
            {
                throw new NoContentEx("empresa invalida");
            }

            var existe = await repository.Buscar(empresa, dto);
            if(existe != null)
            {
                throw new NotFoundEx($"Esta categoria existe con el Id {existe.Id}");
            }
            var categoria = Mapper.CategoriaMapper.AModelo(dto);

            await repository.Crear(categoria);
        }

        public async Task<CategoriaDto>ObtenerPorId(int Id)
        {
            var empresa = await repository.BuscarEmpresa();

            var categoria = await repository.ObtenerPorId(Id, empresa);
            if (categoria == null)
            {
                throw new NoContentEx("Categoria no encontrada");
            }
            var dto = Mapper.CategoriaMapper.ACategoriaDto(categoria);

            return dto;
        }

        public async Task<List<LeerCategoriaDto>> Get()
        {
            var empresa = await repository.BuscarEmpresa();

            var categoria = await repository.Get(empresa);
            if(categoria == null)
            {
                throw new NoContentEx("Categorias no encontradas");
            }
            var dtos = Mapper.CategoriaMapper.ALeerCategoriaDto(categoria);
            return dtos;
        }

        public async Task Eliminar(int id)
        {
            var empresa = await repository.BuscarEmpresa();
            if (empresa == null)
            {
                throw new NoContentEx("empresa invalida");
            }
            var categoria = await repository.ObtenerPorId(id, empresa);
            if (categoria == null)
            {
                throw new NoContentEx("Categoria No encontrada");
            }
            await repository.Eliminar(categoria);
        }
    }
}
