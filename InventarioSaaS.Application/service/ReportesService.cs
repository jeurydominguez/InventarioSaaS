using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Application.service
{
    public class ReportesService : IReporteService
    {
        private readonly IReportesRepository repository;
        public ReportesService(IReportesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<VentasPorDiaDto>> VentasPorDia()
        {
            var empresa = await repository.BuscarEmpresa();
            if(empresa == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }
            int empresaId = int.Parse(empresa);
            var hoyLocal = DateTime.Now.Date;
            var finalLocal = hoyLocal.AddDays(1);

            var hoyUtc = hoyLocal.ToUniversalTime();
            var finalUtc = finalLocal.ToUniversalTime();

            var ventas = await repository.VentasPorDia(hoyUtc, finalUtc, empresaId);

            var dtos = Mapper.ReportesMaper.AVentasPorDiaDto(ventas);
            return dtos; //recordatorio , cambiar las vistas a DateTime.Now para mejor visivilidad 
        }

        public async Task<List<VentasPorDiaDto>> VentaPorRango(DateTime inicio, DateTime final)
        {
            var empresa = await repository.BuscarEmpresa();
            if (empresa == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }
            int empresaId = int.Parse(empresa);
            var inicioUtc = inicio.ToUniversalTime();
            var finalUtc = final.ToUniversalTime();

            var ventas = await repository.VentasPorDia(inicioUtc, finalUtc, empresaId);
            if( ventas.Count == 0)
            {
                throw new NoContentEx($"No hay ventas desde: {inicio} hasta: {final}");
            }
            var dtos = Mapper.ReportesMaper.AVentasPorDiaDto(ventas);
            return dtos;
        }

        public async Task<List<ProductoTop5Dto>> ProductoMasVendido(DateTime inicio, DateTime final)
        {
            var empresa = await repository.BuscarEmpresa();
            int empresaId = int.Parse(empresa);

            var incioUtc = inicio.ToUniversalTime();
            var finalUtc = final.ToUniversalTime();
            var productos = await repository.ProductosMasVendidos(incioUtc, finalUtc, empresaId);
            return productos;
        }

        public async Task<List<ClientesCondeudaDto>> ClientesConDeuda()
        {
            var empresa = await repository.BuscarEmpresa();
            int empresaId = int.Parse(empresa);
            if (empresa == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }

            var clientes = await repository.ClientesConDeuda(empresaId);
            var dtos = Mapper.ReportesMaper.AClientesConDeuda(clientes);
            return dtos;
        }

        public async Task<EstadoCuentasDtos> ReporteDeEstadoDeCuentas()
        {
            var empresa = await repository.BuscarEmpresa();
            int empresaId = int.Parse(empresa);
            if (empresa == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }
            var estado = await repository.EstadoDeCuentasPorCobrar(empresaId);
            return estado;
        }

        public async Task<GananciaNetaDto>ObtenerGanaciaNeta(DateTime inicio, DateTime final)
        {
            var empresa = await repository.BuscarEmpresa();
            int empresaId = int.Parse(empresa);
            if (empresa == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }

            var ganacia = await repository.ObtenerGanaciaNeta(inicio, final, empresaId);
            return ganacia;
        }
        public async Task<ReporteResumenDto>ObtenerResumen(DateTime inicio, DateTime final)
        {
            var empresa = await repository.BuscarEmpresa();
            int empresaId = int.Parse(empresa);
            if (empresa == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }

            var resumen = await repository.ObtenerResumen(inicio, final, empresaId);
            return resumen;
        }

        public async Task<List<VentaChartDto>>ObtenerVentaRango(DateTime inicio, DateTime final)
        {
            var empresa = await repository.BuscarEmpresa();
            int empresaId = int.Parse(empresa);
            if (empresa == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }

            var datos = await repository.VentaPorRango(inicio, final, empresaId);
            return datos;
        }
        public async Task<List<NotificacionDto>> ObtenerNotificaciones()
        {
            var empresaId = int.Parse(await repository.BuscarEmpresa());

            return await repository.ObtenerNotificaciones(empresaId);
        }
    }
}
