using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Application.service
{
    public class CuentasPorCobrarService : ICuentasPorCobrarService
    {
        private readonly CuentasPorCobrarIRepository repository;

        public CuentasPorCobrarService(CuentasPorCobrarIRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<LeerCuentasPorCobrarDto>> Get()
        {
            var claim = await repository.ObtenerEmpresaId();
            if(claim == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }
            int empresaId = int.Parse(claim);

            var cuentasPorCobrar = await repository.Get(empresaId);
            var dtos = Mapper.CuentasPorCobrarMapper.ALeerCuentasPorCobrar(cuentasPorCobrar);
            return dtos;
        }

        public async Task<LeerCuentaPorCobrarUnidadDto> Obtener(int id)
        {
            var claim = await repository.ObtenerEmpresaId();
            if (claim == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }
            int empresaId = int.Parse(claim);

            var cuenta = await repository.ObtenerUno(empresaId, id);
            if(cuenta == null)
            {
                throw new NoContentEx("Cuenta por cobrar no valida");
            }
            var dto = Mapper.CuentasPorCobrarMapper.ALeerCuentasPorCobrarUnidad(cuenta);
            return dto;
        }
    }
}
