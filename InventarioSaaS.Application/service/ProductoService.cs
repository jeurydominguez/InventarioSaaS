using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public async Task<EditarProductoDto> Editar(int id)
        {
            var empresaId = await repository.BuscarClaimEmpresaID();
            if(empresaId == null)
            {
                throw new NotFoundEx("No se pudo obtener el id de la empresa");
            }
            int idEmpresa = int.Parse(empresaId);

            var productoEncontrado = await repository.BuscarProducto(idEmpresa, id); //se busca el producto la primera vez por que necesito saber si el producto existe en el contexto actual
            if (productoEncontrado == null && productoEncontrado.EmpresaId != idEmpresa)
            {
                throw new NoContentEx("Producto no encontrado");
            }

            var productoDto = Mapper.ProductoMapper.AEditarProductoDto(productoEncontrado);// lo convertimos a Dto para enviarlo al controller y aplicarle el parche
            return productoDto;
        }

        public async Task Actualizar(EditarProductoDto dto)
        {
            var producto = await repository.BuscarProducto(dto.EmpresaId, dto.Id);//se busca una segunda vez para tener el producto original de la base de datos 
            if(producto == null && producto.EmpresaId != dto.EmpresaId)
            {
                throw new NoContentEx("Producto no encontrado");
            }

            //se aplican los cambios que recibo del dto ya parcheado desde el controller
            producto.Nombre = dto.Nombre;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.Stock = dto.Stock;

            //aqui termina todo , es complicado por cuestiones de logica pero funciona 10/10
            await repository.Editar(producto);
        }

        public async Task<LeerProductoDto> BuscarProductoPorId(int id)
        {
            var empresaId = await repository.BuscarClaimEmpresaID();
            if (empresaId == null)
            {
                throw new NotFoundEx("No se pudo obtener el id de la empresa");
            }
            int IdEmpresa = int.Parse(empresaId);

            var producto = await repository.BuscarProducto(IdEmpresa, id);
            if(producto == null && producto.EmpresaId != IdEmpresa)
            {
                throw new NoContentEx("Producto no encontrado");
            }

            var dto = Mapper.ProductoMapper.ALeerProductoDto(producto);

            return dto;
        }

        public async Task Eliminar(int id)
        {
            var empresaId = await repository.BuscarClaimEmpresaID();
            if(empresaId == null)
            {
                throw new NoContentEx("Credenciales Incorrectas");
            }
            int idEmpresa = int.Parse(empresaId);

            var producto = await repository.BuscarProducto(idEmpresa, id);
            if(producto == null && producto.EmpresaId != idEmpresa)
            {
                throw new NoContentEx("Producto no encontrado");
            }
            await repository.Eliminar(producto);

        }
    }
}
