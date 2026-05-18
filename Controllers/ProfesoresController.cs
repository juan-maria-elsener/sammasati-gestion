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
    public class ProfesoresController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public ProfesoresController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/Profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesore>>> GetProfesores()
        {
            return await _context.Profesores.ToListAsync();
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesore>> GetProfesore(int id)
        {
            var profesore = await _context.Profesores.FindAsync(id);

            if (profesore == null)
            {
                return NotFound();
            }

            return profesore;
        }

        // PUT: api/Profesores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesore(int id, Profesore profesore)
        {
            // Nota: Si tu propiedad en Profesore.cs se llama IdProfesore, cambialo acá abajo
            if (id != profesore.IdProfesor)
            {
                return BadRequest();
            }

            _context.Entry(profesore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoreExists(id))
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

        // POST: api/Profesores
        [HttpPost]
        public async Task<ActionResult<Profesore>> PostProfesore(Profesore profesore)
        {
            _context.Profesores.Add(profesore);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesore), new { id = profesore.IdProfesor }, profesore);
        }

        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesore(int id)
        {
            var profesore = await _context.Profesores.FindAsync(id);
            if (profesore == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesoreExists(int id)
        {
            return _context.Profesores.Any(e => e.IdProfesor == id);
        }
    }
}