using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
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
    }
}
