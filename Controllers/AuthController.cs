using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sammasati.App.Data;
using Sammasati.App.Models;

namespace Sammasati.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public AuthController(SammasatiDbContext context)
        {
            _context = context;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { mensaje = "El email y la contraseña son obligatorios." });
            }

            // Buscamos si existe un usuario con ese email y esa contraseña exacta
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (usuario == null)
            {
                // Código 401: No autorizado (credenciales incorrectas)
                return Unauthorized(new { mensaje = "Email o contraseña incorrectos." });
            }

            // Si pasa, devolvemos los datos PERO ocultamos la contraseña por seguridad
            return Ok(new
            {
                idUsuario = usuario.IdUsuario,
                email = usuario.Email,
                rol = usuario.Rol,
                mensaje = "Login exitoso"
            });
        }
    }

    // Esta clase sirve como "molde" para recibir los datos desde React
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}