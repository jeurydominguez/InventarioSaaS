using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Application.service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository repository;

        public ProductoService(IProductoRepository repository)
        {
            this.repository = repository;
        }

        //tengo que poner mas verificadores 
        public async Task Crear(CrearProductoDto dto)
        {
            var empresa = await repository.BuscarClaimEmpresaID();

            var product = Mapper.ProductoMapper.AModelo(dto, int.Parse(empresa));
            if(product == null)
            {
                throw new NotFoundEx("El Producto no se pudo crear correctamente");
            }

            await repository.Crear(product);
        }

        public async Task<List<LeerProductoDto>> BuscarTodos()
        {
            var empresaId = await repository.BuscarClaimEmpresaID();
            if(empresaId == null)
            {
                throw new NotFoundEx("No se pudo obtener el id de la empresa");
            }
            int id = int.Parse(empresaId);

            var productos = await repository.BuscarTodos(id);
            if( productos.Count == 0)
            {
                throw new NoContentEx("no tienes productos creados");
            }

            var dtos = Mapper.ProductoMapper.AListaDto(productos);
            return dtos;
        }
    }
}
