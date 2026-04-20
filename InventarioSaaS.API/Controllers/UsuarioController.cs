using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
        [EndpointSummary("Logeamos")]
        public async Task<ActionResult> Login(LogearUsuarioDto dto)
        {
            var token = await servicio.Login(dto);
            return Ok(token);
        }

        //no queria hacerlo de esta forma pero es posible que sea la unica a largo plazo , esperemos no rompa nada 
        [HttpGet()]
        [Authorize(Policy = "admin")]
        [EndpointSummary("Hacemos a un usuario Admin")]
        [Description("para usarla tienes que ser admin")]
        public async Task<IActionResult> HacerAdmin([Description("datos de la cuenta")]HacerAdminDto dto)
        {
            await servicio.HacerAdmin(dto);
            return Ok();
        }
    }
}
