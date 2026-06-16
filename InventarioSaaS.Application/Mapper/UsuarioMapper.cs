
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
                NombreCompleto = dto.NombreUsuario.Replace(" ", "") + " " + dto.Apellido.Replace(" ", ""),
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

        public static UsuarioActualDto AUsuarioMe(Usuario user)
        {
            return new UsuarioActualDto
            {
                Nombre = user.UserName,
                Iniciales = string.Concat(user.NombreCompleto.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(palabra => palabra[0])).ToUpper(),
                Email = user.Email,
                Rol = user.Rol,
            };
        }

        public static UsuarioSettingsDto AUsuarioSettingsDto(Usuario user)
        {
            return new UsuarioSettingsDto
            {
                NombreCompleto = user.NombreCompleto,
                Email = user.Email
            };
        }

        public static Usuario AUsuarioDeCrearUsuarioDto(CrearUsuarioDto dto, Empresa empresa)
        {
            return new Usuario
            {
                Email = dto.Email,
                UserName = dto.NombreUsuario,
                NombreCompleto = dto.NombreUsuario.Replace(" ", "") + " " + dto.Apellido.Replace(" ", ""),
                Rol = dto.rol,
                Empresa = empresa,
                EmpresaId = empresa.Id
            };
        }
    }
}
