using InventarioSaaS.Application.EX;
using InventarioSaaS.Application.service;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaasTest.pruebasUnitarias.Services
{
    [TestClass]
    public class CategoriaServiceTest
    {
        [TestMethod]
        public async Task Crear_SiEmpresaNoEsValida_EsperoNoContext()
        {
            var mockrepo = new Mock<ICategoriaRepository>();
            var dto = new CategoriaDto
            {
                Nombre = "bebida",
                Descripcion = "Alcolica 70%"
            };
            mockrepo.Setup(r => r.BuscarEmpresa())
                .ReturnsAsync(200);

            var service = new CategoriaService(mockrepo.Object);
            Assert.ThrowsAsync<NoContentEx>(() =>
            service.Crear(dto));
        }
        [TestMethod]
        public async Task Crear_SiCategoriaExiste_RetornaNotFound()
        {
            var mockrepo = new Mock<ICategoriaRepository>();
            var dto = new CategoriaDto
            {
                Nombre = "bebida",
                Descripcion = "Alcolica 70%"
            };
            var categoria = new Categoria
            {
                Id = 2,
                Nombre = "bebida",
                Descripcion = "Alcolica 70%"
            };
            mockrepo.Setup(r => r.BuscarEmpresa())
                .ReturnsAsync(1);
            mockrepo.Setup(r => r.Buscar(1, dto))
                .ReturnsAsync(categoria);

            var service = new CategoriaService(mockrepo.Object);
            Assert.ThrowsAsync<NotFoundEx>(() =>
            service.Crear(dto));
        }
        [TestMethod]
        public async Task Crear_SiTodoFuncionaBien()
        {
            var mockrepo = new Mock<ICategoriaRepository>();
            var dto = new CategoriaDto
            {
                Nombre = "bebida",
                Descripcion = "Alcolica 70%"
            };
            mockrepo.Setup(r => r.BuscarEmpresa())
                .ReturnsAsync(1);
            mockrepo.Setup(r => r.Buscar(1, dto))
                .ReturnsAsync((Categoria)null);

            var service = new CategoriaService(mockrepo.Object);
            await service.Crear(dto);
            mockrepo.Verify(r => r.Crear(It.IsAny<Categoria>()), Times.Once);
        }
    }
}
