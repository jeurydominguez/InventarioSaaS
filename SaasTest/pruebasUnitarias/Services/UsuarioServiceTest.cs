
using InventarioSaaS.Application.EX;
using InventarioSaaS.Application.service;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaasTest.pruebasUnitarias.Services
{
    [TestClass]
    public class UsuarioServiceTest
    {
        [TestMethod]
        public async Task Registrar_SiEmpresaExiste_EsperoNotFoundEx()
        {
            var mockrepo = new Mock<IUsuarioRepository>();
            var mocConfig = new Mock<IConfiguration>();
            var dto = new RegistrarUsuarioDTO
            {
                NombreEmpresa = "EmpresaDeAlguien",
                Email = "alguien@gmail.com",
                EmpresaEmail = "SoftEngine@gmail.com",
                NombreUsuario = "luis montilla"
            };
            var empresa = new Empresa
            {
                Nombre = "Empresa",
                Email = "jeurydominguez53@gmail.com",
                FechaCreacion = DateTime.Now,
                Estado = "activo"
            };
            mockrepo.Setup(r => r.BuscarEmpresa("SoftEngine@gmail.com"))
                .ReturnsAsync(empresa);

            mockrepo.Setup(r => r.BuscarUsuario(dto.Email))
                .ReturnsAsync((Usuario)null);
            var service = new UsuarioService(mockrepo.Object, mocConfig.Object);
            await Assert.ThrowsAsync<NotFoundEx>(() =>
            service.Registrar(dto));
        }

        [TestMethod]
        public async Task CrearToken_SiUsuarioNoExiste_EsperoNoContext()
        {
            var mockrepo = new Mock<IUsuarioRepository>();
            var mocConfig = new Mock<IConfiguration>();
            var dto = new RegistrarUsuarioDTO
            {
                NombreEmpresa = "EmpresaDeAlguien",
                Email = "alguien@gmail.com",
                EmpresaEmail = "SoftEngine@gmail.com",
                NombreUsuario = "luis montilla"
            };
            mockrepo.Setup(r => r.BuscarUsuario(dto.Email))
                .ReturnsAsync((Usuario)null);

            var service = new UsuarioService(mockrepo.Object, mocConfig.Object);
            await Assert.ThrowsAsync<NoContentEx>(() =>
            service.CrearToken(dto));
        }
    }
}
