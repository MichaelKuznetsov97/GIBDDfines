using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GIBDDfines.Models;

namespace GIBDDfines.Controllers
{
    [Produces("application/json")]
    [Route("api/Punishments")]
    public class PunishmentsController : Controller
    {
        private readonly modeldbGIBDDContext _context;

        public PunishmentsController(modeldbGIBDDContext context)
        {
            _context = context;
        }

        // GET: api/Punishments
        [HttpGet]
        public IEnumerable<Punishments> GetPunishments()
        {
            return _context.Punishments;
        }

        // GET: api/Punishments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPunishments([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var punishments = await _context.Punishments.SingleOrDefaultAsync(m => m.Id == id);

            if (punishments == null)
            {
                return NotFound();
            }

            return Ok(punishments);
        }

        //изменение нарушения(псевдоПУТ запрос, реализован как ГЕТ)
        // PUT: api/Punishments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPunishments([FromRoute] string id, [FromBody] Punishments punishments)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            punishments = await _context.Punishments.SingleOrDefaultAsync(m => m.Id == id);

            if (id != punishments.Id)
                return BadRequest();

            punishments.DatePay = DateTime.Now;

            _context.Entry(punishments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunishmentsExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        //добавление нарушения
        // POST: api/Punishments
        [HttpPost]
        public async Task<IActionResult> PostPunishments([FromBody] Punishments punishments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Punishments.Add(punishments);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PunishmentsExists(punishments.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPunishments", new { id = punishments.Id }, punishments);
        }

        // DELETE: api/Punishments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePunishments([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var punishments = await _context.Punishments.SingleOrDefaultAsync(m => m.Id == id);
            if (punishments == null)
            {
                return NotFound();
            }

            _context.Punishments.Remove(punishments);
            await _context.SaveChangesAsync();

            return Ok(punishments);
        }

        private bool PunishmentsExists(string id)
        {
            return _context.Punishments.Any(e => e.Id == id);
        }
    }
}