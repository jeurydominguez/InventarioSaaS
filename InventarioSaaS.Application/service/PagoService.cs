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
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository repository;
        public PagoService(IPagoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<LeerPagoDto>> GetAll()
        {
            var empresaId = await repository.ObtenerEmpresaId();
            if(empresaId == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }
            var pagos = await repository.GetAll(empresaId);

            var dtos = Mapper.PagoMapper.ALeerPagoDto(pagos);
            return dtos;
        }

        public async Task<LeerPagoDtoUnidad> Get(int id)
        {
            var empresaId = await repository.ObtenerEmpresaId();
            if (empresaId == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }

            var pago = await repository.ObtenerPorId(empresaId, id);
            if (pago == null)
            {
                throw new NoContentEx("Pago no encontrados");
            }
            var dto = Mapper.PagoMapper.ALeerPagoDtoUnidad(pago);
            return dto;
        }

        public async Task CrearPago(CrearPagoDto dto)
        {
            var empresaId = await repository.ObtenerEmpresaId();
            if (empresaId == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }
            dto.EmpresaId = empresaId;
            var cuentaPorCobrar = await repository.ObtenerCuentaPorCobrar(empresaId, dto.CuentaPorCobrarId);
            if (cuentaPorCobrar == null)
            {
                throw new NoContentEx("Cuenta No encontrada");
            }
            Calculos(dto, cuentaPorCobrar);
            var Pago = Mapper.PagoMapper.AModeloPago(dto);
            await repository.GuardarPago(Pago, cuentaPorCobrar);
        }

        public void Calculos(CrearPagoDto dto, CuentasPorCobrar cuenta)
        {
            if(dto.Monto <= 0 || dto.Monto > cuenta.MontoPendiente)
            {
                throw new NotFoundEx("El monto no es valido");
            }
            cuenta.MontoPendiente = cuenta.MontoPendiente - dto.Monto;
            if (cuenta.MontoPendiente == 0)
            {
                cuenta.Estado = TipoPago.Estado.Pagado;
            }
        }
    }
}
