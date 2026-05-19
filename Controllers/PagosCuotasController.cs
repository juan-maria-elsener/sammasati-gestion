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
    public class PagosCuotasController : ControllerBase
    {
        private readonly SammasatiDbContext _context;

        public PagosCuotasController(SammasatiDbContext context)
        {
            _context = context;
        }

        // GET: api/PagosCuotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagosCuota>>> GetPagosCuotas()
        {
            return await _context.PagosCuotas.ToListAsync();
        }

        // GET: api/PagosCuotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PagosCuota>> GetPagosCuota(int id)
        {
            var pagosCuota = await _context.PagosCuotas.FindAsync(id);

            if (pagosCuota == null)
            {
                return NotFound();
            }

            return pagosCuota;
        }

        // PUT: api/PagosCuotas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagosCuota(int id, PagosCuota pagosCuota)
        {
            if (id != pagosCuota.IdPago)
            {
                return BadRequest();
            }

            _context.Entry(pagosCuota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagosCuotaExists(id))
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

        // POST: api/PagosCuotas
        [HttpPost]
        public async Task<ActionResult<PagosCuota>> PostPagosCuota(PagosCuota pagosCuota)
        {
            _context.PagosCuotas.Add(pagosCuota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPagosCuota), new { id = pagosCuota.IdPago }, pagosCuota);
        }

        // DELETE: api/PagosCuotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagosCuota(int id)
        {
            var pagosCuota = await _context.PagosCuotas.FindAsync(id);
            if (pagosCuota == null)
            {
                return NotFound();
            }

            _context.PagosCuotas.Remove(pagosCuota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/PagosCuotas/pendientes
        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<PagosCuota>>> GetPagosPendientes()
        {
            // Uso LINQ para filtrar en la base de datos antes de traer los datos a la memoria
            return await _context.PagosCuotas
                .Where(p => p.Estado == "Pendiente")
                .ToListAsync();
        }

        // PUT: api/PagosCuotas/cobrar/5
        [HttpPut("cobrar/{id}")]
        public async Task<IActionResult> CobrarCuota(int id)
        {
            // Buscamos el registro de pago específico por su ID
            var pago = await _context.PagosCuotas.FindAsync(id);

            if (pago == null)
            {
                return NotFound($"No se encontró ningún registro de pago con el ID: {id}");
            }

            // Cambiamos el estado a Abonado de forma automática
            pago.Estado = "Abonado";

            // Le avisamos a Entity Framework que este registro sufrió modificaciones
            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagosCuotaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Devolvemos un mensaje de éxito junto con el objeto actualizado
            return Ok(new { Mensaje = "Cuota cobrada con éxito", PagoActualizado = pago });
        }

        private bool PagosCuotaExists(int id)
        {
            return _context.PagosCuotas.Any(e => e.IdPago == id);
        }
    }
}