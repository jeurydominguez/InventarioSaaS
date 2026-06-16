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
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository repository;

        public SettingsService(ISettingsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<EmpresaSettingsDto> BuscarEmpresa()
        {
            var empresaId = await repository.ObtenerEmpresaId();
            if (empresaId == null)
            {
                throw new NoContentEx("Credenciales no validas");
            }

            var empresa = await repository.BuscarEmpresa(empresaId);
            var empresaDto = Mapper.EmpresaMapper.AEmpresaSettingsDto(empresa);

            return empresaDto;
        }

        public async Task Editar(EmpresaSettingsDto dto)
        {
            var empresaId = await repository.ObtenerEmpresaId();
            var empresa = await repository.BuscarEmpresa(empresaId);

            empresa.Nombre = dto.Nombre;
            empresa.Email = dto.Email;

            await repository.Actualizar(empresa);
        }

        public async Task<UsuarioSettingsDto> BuscarUsuario()
        {
            var usuario = await repository.BuscarUsuario();
            if(usuario == null)
            {
                throw new NoContentEx("Usuario no encontrado");
            }

            var dto = Mapper.UsuarioMapper.AUsuarioSettingsDto(usuario);
            return dto;
        }
    }
}
