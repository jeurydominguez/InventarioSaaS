using InventarioSaaS.Application.EX;
using InventarioSaaS.Application.service;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaasTest.pruebasUnitarias.Controllers
{
    //[DataRow] sirve para poner varios valores
    [TestClass]
    public class ObtenerProductoPorIdPrueva
    {
        [TestMethod]
        public async Task BuscarProductoPorId_SinEmpresa_LanzaNotFound()
        {
            var mockrepo = new Mock<IProductoRepository>();
            mockrepo.Setup(r => r.BuscarClaimEmpresaID())
                .ReturnsAsync((int?)null);

            var service = new ProductoService(mockrepo.Object);

            await Assert.ThrowsAsync<NotFoundEx>(() =>
            service.BuscarProductoPorId(1));
        }

        [TestMethod]
        public async Task BuscarProductoPorId_IdNoConicide_LanzaNotContext()
        {
            var mockrepo = new Mock<IProductoRepository>();
            mockrepo.Setup(r => r.BuscarClaimEmpresaID())
                .ReturnsAsync(1);

            mockrepo.Setup(r => r.BuscarProducto(1, 60))
                .ReturnsAsync((Producto)null);

            var service = new ProductoService(mockrepo.Object);

            await Assert.ThrowsAsync<NoContentEx>(() =>
            service.BuscarProductoPorId(60));
        }

        [TestMethod]
        public async Task BuscarProductoPorId_TodoCorrecto_EsperoUnDto()
        {
            var mockrepo = new Mock<IProductoRepository>();
            var producto = new Producto
            {
                Id = 1,
                Nombre = "teclado",
                EmpresaId = 1,
                PrecioVenta = 500.00m,
                Categoria = new Categoria
                {
                    Id = 10,
                    EmpresaId = 1,
                    Nombre = "teconologia",
                    Descripcion = "es un teclado"
                },
                Stock = 3
            };
            mockrepo.Setup(r => r.BuscarClaimEmpresaID())
                .ReturnsAsync(1);
            mockrepo.Setup(r => r.BuscarProducto(1, 1))
                .ReturnsAsync(producto);

            var service = new ProductoService(mockrepo.Object);
            var resultado = await service.BuscarProductoPorId(1);

            Assert.IsNotNull(resultado);
        }
    }
}
