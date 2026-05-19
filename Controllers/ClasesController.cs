using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sammasati.App.Data;
using Sammasati.App.Models;

namespace Sammasati.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public ClasesController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/Clases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clase>>> GetClases()
        {
            return await _context.Clases.ToListAsync();
        }

        // GET: api/Clases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clase>> GetClase(int id)
        {
            var clase = await _context.Clases.FindAsync(id);

            if (clase == null)
            {
                return NotFound();
            }

            return clase;
        }

        // PUT: api/Clases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClase(int id, Clase clase)
        {
            
            if (id != clase.IdClase)
            {
                return BadRequest();
            }

            _context.Entry(clase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clases
        [HttpPost]
        public async Task<ActionResult<Clase>> PostClase(Clase clase)
        {
            _context.Clases.Add(clase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClase), new { id = clase.IdClase }, clase);
        }

        // DELETE: api/Clases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClase(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }

            _context.Clases.Remove(clase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Clases/detalles
        [HttpGet("detalles")]
        public async Task<IActionResult> GetClasesDetalles()
        {
            
            var clasesDetalladas = await _context.Clases
                .Select(c => new
                {
                    c.IdClase,
                    c.Dias,
                    c.Horario,
                    Profesor = c.IdProfesorNavigation != null ? c.IdProfesorNavigation.Nombre : "Sin Profesor",
                    Espacio = c.IdEspacioNavigation != null ? c.IdEspacioNavigation.NombreDireccion : "Sin Espacio",
                    Modalidad = c.IdModalidadNavigation != null ? c.IdModalidadNavigation.Nombre : "Sin Modalidad"
                })
                .ToListAsync();

            return Ok(clasesDetalladas);
        }

        private bool ClaseExists(int id)
        {
            return _context.Clases.Any(e => e.IdClase == id);
        }
    }
}