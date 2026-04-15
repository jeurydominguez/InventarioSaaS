using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.XPath;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService service;
        public ClienteController(IClienteService service)
        {
            this.service = service;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await service.BuscarPorId(id);
            return Ok(cliente);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CrearClienteDto dto)
        {
            await service.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, dto);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await service.ObtenerTodos();
            return Ok(dtos);
        }

        [HttpPatch("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Actualizar(int id, JsonPatchDocument<ActualizarClienteDto> jsonPatch)
        {
            if (jsonPatch == null)
            {
                return NoContent();
            }
            var dto = await service.Actualizar(id);
            jsonPatch.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
            {
                return NotFound("Problema al actualizar");
            }
            await service.Editar(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = id }, jsonPatch);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await service.Eliminar(id);
            return NoContent();
        }
    }
}
