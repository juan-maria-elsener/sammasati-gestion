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
    public class InscripcionesController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public InscripcionesController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/Inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscripcione>>> GetInscripciones()
        {
            return await _context.Inscripciones.ToListAsync();
        }

        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcione>> GetInscripcione(int id)
        {
            var inscripcione = await _context.Inscripciones.FindAsync(id);

            if (inscripcione == null)
            {
                return NotFound();
            }

            return inscripcione;
        }

        // PUT: api/Inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcione(int id, Inscripcione inscripcione)
        {
            if (id != inscripcione.IdInscripcion)
            {
                return BadRequest();
            }

            _context.Entry(inscripcione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcioneExists(id))
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

        // POST: api/Inscripciones
        [HttpPost]
        public async Task<ActionResult<Inscripcione>> PostInscripcion(Inscripcione inscripcion)
        {
            // 1. Contamos cuántos alumnos ya están activos en esta clase exacta
            var alumnosActivos = await _context.Inscripciones
                .CountAsync(i => i.IdClase == inscripcion.IdClase && i.Estado == "Activa");

            // 2. Definimos el límite de colchonetas físicas (podés cambiar este número)
            int cupoMaximo = 17;

            // 3. REGLA DE NEGOCIO: Si ya está lleno, lo mandamos a lista de espera
            if (alumnosActivos >= cupoMaximo)
            {
                inscripcion.Estado = "Lista de Espera";
            }
            else
            {
                // Si hay lugar, le forzamos el estado a Activa por las dudas
                inscripcion.Estado = "Activa";
            }

            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();

            // Devolvemos la inscripción guardada (para que React sepa qué estado le tocó al final)
            return Ok(inscripcion);
        }

        // DELETE: api/Inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcione(int id)
        {
            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            _context.Inscripciones.Remove(inscripcione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscripcioneExists(int id)
        {
            return _context.Inscripciones.Any(e => e.IdInscripcion == id);
        }
    }
}