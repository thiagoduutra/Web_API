using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto.Usuario;
using WebAPI.Services.Usuario;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public LoginController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(UsuarioCriarDto usuarioCriarDto)
        {
            var user = await _usuarioInterface.CreateUser(usuarioCriarDto);
            return Ok(user);
        }
    }
}
