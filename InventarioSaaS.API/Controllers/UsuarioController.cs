using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService servicio;

        public UsuarioController(IUsuarioService servicio)
        {
            this.servicio = servicio;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult>Registrar(RegistrarUsuarioDTO dto)
        {
            var resultado = await servicio.Registrar(dto);
            return Ok(resultado);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LogearUsuarioDto dto)
        {
            var token = await servicio.Login(dto);
            return Ok(token);
        }

        [HttpPost("hacer-admin")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> HacerAdmin(HacerAdminDto dto)
        {
            await servicio.HacerAdmin(dto);
            return Ok();
        }
    }
}
