using InventarioSaaS.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IUsuarioService
    {
        Task Registrar(RegistrarUsuarioDTO dto);
        Task<TokenDto> CrearToken(RegistrarUsuarioDTO dto);
        Task<TokenDto> Login(LogearUsuarioDto dto);
        Task HacerAdmin(HacerAdminDto dto);
        Task<UsuarioActualDto> Me();
        Task<IdentityResult> CrearUsuario(CrearUsuarioDto dto);
        Task ConfirmarEmail(string userId, string token);
        Task ReenviarConfirmacion(string email);
    }
}
