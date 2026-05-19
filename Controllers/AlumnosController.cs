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
    public class AlumnosController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public AlumnosController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/Alumnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

        // GET: api/Alumnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }

        // PUT: api/Alumnos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno alumno)
        {
            if (id != alumno.IdAlumno)
            {
                return BadRequest();
            }

            _context.Entry(alumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
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

        // POST: api/Alumnos
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlumno), new { id = alumno.IdAlumno }, alumno);
        }

        // DELETE: api/Alumnos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Alumnos/5/historial
        [HttpGet("{id}/historial")]
        public async Task<IActionResult> GetFichaAlumno(int id)
        {
            // Buscamos el alumno y proyectamos un objeto con todo su historial unificado
            var fichaAlumno = await _context.Alumnos
                .Where(a => a.IdAlumno == id)
                .Select(a => new
                {
                    a.IdAlumno,
                    a.Nombre, // O el campo que use tu modelo para el nombre completo

                    // Traemos la lista de clases a las que se inscribió este alumno
                    ClasesAsiste = a.Inscripciones.Select(i => new
                    {
                        i.IdClase,
                        Dia = i.IdClaseNavigation != null ? i.IdClaseNavigation.Dias : "Sin Asignar",
                        Horario = i.IdClaseNavigation != null ? i.IdClaseNavigation.Horario.ToString(): "Sin Asignar"
                    }).ToList(),

                    // Buscamos en la tabla de pagos todas las cuotas asociadas a este alumno
                    HistorialPagos = _context.PagosCuotas
                        .Where(p => p.IdAlumno == id)
                        .Select(p => new
                        {
                            p.IdPago, // Ajustar si tu clave primaria se llama IdPagoCuota o similar
                            p.Mes,
                            p.Estado
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (fichaAlumno == null)
            {
                return NotFound($"No se encontró ningún alumno con el ID: {id}");
            }

            return Ok(fichaAlumno);
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.IdAlumno == id);
        }
    }
}