using InventarioSaaS.Domain.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Infrastructure.ApplicationDbContext
{
    public class AppDbcontext : IdentityDbContext<Usuario>
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {

        }

        public DbSet<Empresa> Empresa { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //llama a la configuracion base, se esta usando identity es decir que llama la configuracion de identity
            base.OnModelCreating(builder);
            
            builder.Entity<Usuario>()// se le dice como "ok , voy a configurar la entidad usuario"
                .HasOne(u => u.Empresa)// un usuario tiene 1 empresa
                .WithMany()//una empresa puede tener varios usuarios
                .HasForeignKey(u => u.EmpresaId)// se define la llave foranea , la que conecta todos los usuarios con la empresa
                .OnDelete(DeleteBehavior.Restrict);//no puedes eliminar una empresa si tiene usuarios asociados 
        }
    }
}
