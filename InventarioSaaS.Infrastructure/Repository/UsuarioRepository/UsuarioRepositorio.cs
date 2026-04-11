
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace InventarioSaaS.Infrastructure.Repository.UsuarioRepository
{
    public class UsuarioRepositorio : IUsuarioRepository
    {
        private readonly UserManager<Usuario> userManager;
        private readonly AppDbcontext dbContext;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IHttpContextAccessor httpContext;

        public UsuarioRepositorio(UserManager<Usuario> userManager, AppDbcontext dbcontext, SignInManager<Usuario> signInManager, IHttpContextAccessor httpContext)
        {
            this.userManager = userManager;
            this.dbContext = dbcontext;
            this.signInManager = signInManager;
            this.httpContext = httpContext;
        }

        public async Task<Empresa> BuscarEmpresa(string email)
        {
            return await dbContext.Empresa.FirstOrDefaultAsync(e => e.Email == email);//FirstOrDefaultAsync busca por columna ,mientras que findAsync busca por llave primaria 
        }

        public async Task GuardarEmpresa(Empresa empresa)
        {
            await dbContext.Empresa.AddAsync(empresa);
            dbContext.SaveChanges();
        }

        public async Task<IdentityResult> CrearUsuario(Usuario user, RegistrarUsuarioDTO dto)
        {
            var result = await userManager.CreateAsync(user, dto.PassWord);
            return result;
        }

        public async Task<Usuario> BuscarUsuario(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<Claim> BuscarClaimRol()
        {
            var claim = httpContext.HttpContext!.User.Claims.Where(p => p.Type == "rol").FirstOrDefault();
            return claim;
        }

        public async Task<IList<Claim>> ObtenerCLaims(Usuario user)
        {
            var claims = await userManager.GetClaimsAsync(user);
            return claims;
        }

        public async Task<SignInResult> ChekearPassword(Usuario user, LogearUsuarioDto dto)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            return result;
        }

        public async Task<Usuario>BuscarUsuarioConEmpresa(string email)
        {
            var user = await dbContext.Users.Include(e => e.Empresa).FirstOrDefaultAsync(u=> u.Email == email);
            return user;
        }
    }
}
