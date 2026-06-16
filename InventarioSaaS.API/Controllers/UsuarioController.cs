using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
            await servicio.Registrar(dto);
            return Ok();
        }

        [HttpPost("login")]
        [EndpointSummary("Logeamos")]
        public async Task<ActionResult> Login(LogearUsuarioDto dto)
        {
            try
            {
                var token = await servicio.Login(dto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //no queria hacerlo de esta forma pero es posible que sea la unica a largo plazo , esperemos no rompa nada 
        [HttpGet]
        [Authorize(Policy = "admin")]
        [EndpointSummary("Hacemos a un usuario Admin")]
        [Description("para usarla tienes que ser admin")]
        public async Task<IActionResult> HacerAdmin([Description("datos de la cuenta")]HacerAdminDto dto)
        {
            await servicio.HacerAdmin(dto);
            return Ok();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var usuario = await servicio.Me();

            return Ok(usuario);
        }

        [HttpPost("crear")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult>CrearUsuario(CrearUsuarioDto dto)
        {
            var resultado = await servicio.CrearUsuario(dto);
            return Ok(resultado);
        }

        [HttpGet("confirmar-email")]
        public async Task<IActionResult> ConfirmarEmail(string userId, string token)
        {
            await servicio.ConfirmarEmail(userId, token);
            return Ok("email Confirmado");
        }
        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ReenviarConfirmacion(string email)
        {
            await servicio.ReenviarConfirmacion(email);

            return NoContent();
        }
    }
}
