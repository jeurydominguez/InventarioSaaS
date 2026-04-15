using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Application.service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository repository;
        public ClienteService(IClienteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<LeerClienteDto> BuscarPorId(int id)
        {
            var claim = await repository.BuscarEmpresaId();
            if(claim == null)
            {
                throw new NotFoundEx("Credenciales no validas");
            }
            int empresaId = int.Parse(claim);

            var cliente = await repository.ObtenerPorId(empresaId, id);
            if(cliente == null)
            {
                throw new NoContentEx("Cliente no encontrado");
            }
            var dto = Mapper.ClienteMapper.ALeerDto(cliente);
            return dto;
        }
        public async Task Crear(CrearClienteDto dto)
        {
            var claim = await repository.BuscarEmpresaId();
            if(claim == null)
            {
                throw new NotFoundEx("Credenciales no validas");
            }
            int empresaId = int.Parse(claim);

            var cliente = Mapper.ClienteMapper.AModelo(dto, empresaId);
            await repository.Crear(cliente);
        }

        public async Task<List<LeerClienteDto>> ObtenerTodos()
        {
            var id = await repository.BuscarEmpresaId();
            if (id == null)
            {
                throw new NotFoundEx("Credenciales no validas");
            }
            int empresaId = int.Parse(id);

            var clientes = await repository.ObtenerTodo(empresaId);
            if (clientes.Count == 0)
            {
                throw new NotFoundEx("Credenciales no validas");
            }

            var dtos = Mapper.ClienteMapper.AListaLeerCliente(clientes);
            return dtos;
        }

        public async Task<ActualizarClienteDto> Actualizar(int id)
        {
            var idEmpresa = await repository.BuscarEmpresaId();
            if(idEmpresa == null)
            {
                throw new NotFoundEx("Credenciales no validas");
            }
            int empresaId = int.Parse(idEmpresa);

            var cliente = await repository.ObtenerPorId(empresaId, id);
            if(cliente == null)
            {
                throw new NoContentEx("Cliente no encontrado");
            }

            var dto = Mapper.ClienteMapper.AActualizarClienteDto(cliente);
            return dto;
        }

        public async Task Editar(ActualizarClienteDto dto)
        {
            var cliente = await repository.ObtenerPorId(dto.EmpresaId, dto.Id);
            if(cliente == null)
            {
                throw new NoContentEx("Cliente No encontrado");
            }
            cliente.Nombre = dto.Nombre;
            cliente.NumeroTelefono = dto.NumeroTelefono;
            cliente.Direccion = dto.Direccion;

            await repository.Actualizar(cliente);
        }

        public async Task Eliminar(int id)
        {
            var empresa = await repository.BuscarEmpresaId();
            if (empresa == null)
            {
                throw new NotFoundEx("Credenciales no validas");
            }
            int empresaId = int.Parse(empresa);

            var cliente = await repository.ObtenerPorId(empresaId, id);
            if(cliente == null)
            {
                throw new NoContentEx("Cliente No encontrado");
            }

            await repository.Eliminar(cliente);
        }
    }
}
