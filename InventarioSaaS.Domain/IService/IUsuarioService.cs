using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IUsuarioService
    {
        Task<TokenDto> Registrar(RegistrarUsuarioDTO dto);
        Task<TokenDto> CrearToken(RegistrarUsuarioDTO dto);
        Task<TokenDto> Login(LogearUsuarioDto dto);
    }
}
