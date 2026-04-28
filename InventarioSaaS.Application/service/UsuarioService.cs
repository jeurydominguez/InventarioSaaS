using InventarioSaaS.Application.EX;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InventarioSaaS.Application.service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository repository;
        private readonly IConfiguration configuration;
        public UsuarioService(IUsuarioRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<TokenDto> Registrar(RegistrarUsuarioDTO dto)
        {
            var exist = await repository.BuscarEmpresa(dto.EmpresaEmail);
            var existUser = await repository.BuscarUsuario(dto.Email);

            if(existUser != null)
            {
                throw new NotFoundEx("Usuario no valido");
            }

            if(exist != null)
            {
                throw new NotFoundEx("Empresa no valida");
            }

            var empresa = new Empresa
            {
                Nombre = dto.NombreEmpresa,
                Email = dto.EmpresaEmail,
                FechaCreacion = DateTime.UtcNow,
                Estado = "activo"
            };
            await repository.GuardarEmpresa(empresa);

            dto.rol = "admin";
            var usuario = Mapper.UsuarioMapper.AUsuario(dto, empresa);

            //tomara sentido mas adelante por que habra un endpoint para poder crear los usuarios que no son admins de esa empresa pero para un usuario admin, es decir , ese registrar token funcionara siempre
            var result = await repository.CrearUsuario(usuario, dto);
            if (result.Succeeded)
            {
                // aqui creamos el JWT
                var token = await CrearToken(dto);
                return token;
            }
            else
            {
                var errores = string.Join(", ", result.Errors.Select(x => x.Description));
                throw new Exception(errores);
            }
        }
        public async Task<TokenDto> CrearToken(RegistrarUsuarioDTO dto)
        {
            var usuario = await repository.BuscarUsuario(dto.Email);//Buscamos el usuario aunque creo que podria pasarlo desde la anterior clase , pero bueno , doble seguridad supongo
            if(usuario == null)
            {
                throw new NoContentEx("Usuario NO valido");
            }

            var claims = new List<Claim>//las famosas claims , creo que la de empresaEmail es irrelevante pero la dejaremos en el desarrollo para ver si usamos una forma de autenticarla por ahi
            {
                new Claim("Id", usuario.Id),
                new Claim("Email", usuario.Email!),
                new Claim("EmpresaEmail", usuario.Empresa.Email),
                new Claim("NombreUsuario", usuario.UserName!),
                new Claim("EmpresaId", usuario.EmpresaId.ToString()),
                new Claim("rol", usuario.Rol)
            };

            var claimsDB = await repository.ObtenerCLaims(usuario);
            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));//la clave que firma el JWT
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);//la encriptacion

            var fechaExpiracion = DateTime.UtcNow.AddDays(2);//no se , es posible que con este mismo tiempo busquemos la forma de hacer la parte de la suscripcion 

            var tokenSegurdad = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"], audience: configuration["Jwt:Audience"], claims: claims, expires: fechaExpiracion, signingCredentials: credenciales);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenSegurdad);

            return new TokenDto
            {
                Token = token,
                Expiracion = fechaExpiracion
            };
            //final del metodo , retorna el token y la expiracion , todo esta en UTCNOW por cuestion de politicas JWT
        }

        //Login, no se exactamente que tanto puede llegar a escalar el tener que usar LogearUsuarioDto
        public async Task<TokenDto> Login(LogearUsuarioDto dto)
        {
            var exist = await repository.BuscarUsuarioConEmpresa(dto.Email);
            if(exist == null)
            {
                throw new NoContentEx("Usuario no valido");
            }

            var result = await repository.ChekearPassword(exist, dto);
            if (result.Succeeded)
            {
                var paraToken = Mapper.UsuarioMapper.ARegistrarUsuarioDto(exist);
                return await CrearToken(paraToken);
            }
            else
            {
                throw new NoContentEx("usuario no valido");
            }
        }

        public async Task HacerAdmin(HacerAdminDto dto)
        {
            var usuario = await repository.BuscarUsuario(dto.Email);
            var claims = await repository.ObtenerCLaims(usuario);
            var exist = claims.Any(c => c.Type == "rol" && c.Type == "admin");
            if (exist)
            {
                throw new NotFoundEx("el usuario ya es admin");
            }

            await repository.HacerAdmin(usuario);
        }
    }
}
