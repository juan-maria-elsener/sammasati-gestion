using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sammasati.App.Data;

namespace Sammasati.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public DashboardController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/Dashboard/resumen
        [HttpGet("resumen")]
        public async Task<IActionResult> GetResumen()
        {
            // CountAsync() es rapidísimo porque se traduce a un "SELECT COUNT" en MySQL, 
            // sin necesidad de descargar toda la información de las tablas a la memoria de C#.

            var totalAlumnos = await _context.Alumnos.CountAsync();
            var pagosPendientes = await _context.PagosCuotas.CountAsync(p => p.Estado == "Pendiente");
            var totalClases = await _context.Clases.CountAsync();
            var totalProfesores = await _context.Profesores.CountAsync();

            // Devolvemos un objeto dinámico con el resumen exacto
            return Ok(new
            {
                AlumnosRegistrados = totalAlumnos,
                CuotasPendientes = pagosPendientes,
                ClasesActivas = totalClases,
                ProfesoresEnStaff = totalProfesores
            });
        }
    }
}