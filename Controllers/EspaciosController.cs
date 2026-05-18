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
    public class EspaciosController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public EspaciosController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/Espacios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Espacio>>> GetEspacios()
        {
            return await _context.Espacios.ToListAsync();
        }

        // GET: api/Espacios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Espacio>> GetEspacio(int id)
        {
            var espacio = await _context.Espacios.FindAsync(id);

            if (espacio == null)
            {
                return NotFound();
            }

            return espacio;
        }

        // PUT: api/Espacios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspacio(int id, Espacio espacio)
        {
            if (id != espacio.IdEspacio)
            {
                return BadRequest();
            }

            _context.Entry(espacio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspacioExists(id))
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

        // POST: api/Espacios
        [HttpPost]
        public async Task<ActionResult<Espacio>> PostEspacio(Espacio espacio)
        {
            _context.Espacios.Add(espacio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspacio), new { id = espacio.IdEspacio }, espacio);
        }

        // DELETE: api/Espacios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspacio(int id)
        {
            var espacio = await _context.Espacios.FindAsync(id);
            if (espacio == null)
            {
                return NotFound();
            }

            _context.Espacios.Remove(espacio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspacioExists(int id)
        {
            return _context.Espacios.Any(e => e.IdEspacio == id);
        }
    }
}