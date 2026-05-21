using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sammasati.App.Data;
using Sammasati.App.Models; 

namespace Sammasati.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public AsistenciasController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/Asistencias
        // Trae todo el historial de asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetAsistencias()
        {
            return await _context.Asistencias.ToListAsync();
        }

        // POST: api/Asistencias
        // Guarda una asistencia individual
        [HttpPost]
        public async Task<ActionResult<Asistencia>> PostAsistencia(Asistencia asistencia)
        {
            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            return Ok(asistencia);
        }

        // POST: api/Asistencias/lote
        // ENDPOINT AVANZADO: Recibe una lista entera y la guarda toda junta (Ideal para tomar lista)
        [HttpPost("lote")]
        public async Task<IActionResult> GuardarAsistenciasEnLote(List<Asistencia> asistencias)
        {
            if (asistencias == null || !asistencias.Any())
            {
                return BadRequest("La lista de asistencias está vacía.");
            }

            _context.Asistencias.AddRange(asistencias);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Lista de asistencia guardada correctamente." });
        }
    }
}