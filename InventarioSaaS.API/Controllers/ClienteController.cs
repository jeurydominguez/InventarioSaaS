using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.XPath;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService repository;
        public ClienteController(IClienteService repository)
        {
            this.repository = repository;
        }

        [HttpPost("{id:int}")]
        [Authorize]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await repository.BuscarPorId(id);
            return Ok(cliente);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CrearClienteDto dto)
        {
            await repository.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, dto);
        }

        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> GetAll()
        //{

        //}
    }
}
