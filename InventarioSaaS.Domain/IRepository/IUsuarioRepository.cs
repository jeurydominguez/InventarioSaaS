using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace InventarioSaaS.Domain.IRepository
{
    public interface IUsuarioRepository
    {
        Task<Empresa> BuscarEmpresa(string email);
        Task GuardarEmpresa(Empresa empresa);
        Task<IdentityResult> CrearUsuario(Usuario user, RegistrarUsuarioDTO dto);
        Task<Usuario> BuscarUsuario(string email);
        Task<IList<Claim>> ObtenerCLaims(Usuario user);
        Task<SignInResult> ChekearPassword(Usuario user, LogearUsuarioDto dto);
        Task<Claim> BuscarClaimRol();
        Task<Usuario> BuscarUsuarioConEmpresa(string email);
        Task HacerAdmin(Usuario user);

    }
}
