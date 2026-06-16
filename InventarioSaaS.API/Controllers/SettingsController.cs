using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/settings")]

    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService service;

        public SettingsController(ISettingsService service)
        {
            this.service = service;
        }

        [HttpGet("empresa")]
        [Authorize]
        public async Task<IActionResult> ObtenerEmpresa()
        {
            return Ok(await service.BuscarEmpresa());
        }

        [HttpPatch("empresa")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Actualizar(JsonPatchDocument<EmpresaSettingsDto> jsonDocument)
        {
            if(jsonDocument == null)
            {
                return NoContent();
            }

            var dto = await service.BuscarEmpresa();
            jsonDocument.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
            {
                return NotFound("Problemas al actualizar");
            }
            await service.Editar(dto);
            return NoContent();
        }

        [HttpGet("usuario")]
        [Authorize]
        public async Task<IActionResult> ObtenerUsuario()
        {
            return Ok(await service.BuscarUsuario());
        }
    }
}
