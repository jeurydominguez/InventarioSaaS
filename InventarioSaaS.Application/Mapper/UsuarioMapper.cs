
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Application.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario AUsuario(RegistrarUsuarioDTO dto, Empresa empresa)
        {
            return new Usuario
            {
                Email = dto.Email,
                UserName = dto.NombreUsuario,
                NombreCompleto = dto.NombreUsuario + " " + dto.Apellido,
                Rol = dto.rol,
                Empresa = empresa,
                EmpresaId = empresa.Id
            };
        }

        public static RegistrarUsuarioDTO ARegistrarUsuarioDto(Usuario user)
        {
            return new RegistrarUsuarioDTO
            {
                Email = user.Email,
                NombreUsuario = user.UserName,
                EmpresaEmail = user.Empresa.Email,
                NombreEmpresa = user.Empresa.Nombre,
                EmpresaId = user.EmpresaId,
                rol = user.Rol
            };
        }
    }
}
