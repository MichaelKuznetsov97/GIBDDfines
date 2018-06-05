using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GIBDDfines.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GIBDDfines.Controllers
{
    [Produces("application/json")]
    [Route("api/Punishments")]
    public class PunishmentsController : Controller
    {
        private readonly modeldbGIBDD2Context _context;
        private readonly UserManager<User> _userManager;

        public PunishmentsController(modeldbGIBDD2Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Punishments
        [HttpGet]
        public IEnumerable<Punishments> GetPunishments()
        {
            return _context.Punishments;
        }

        // GET: api/Punishmentsenter/semafor
        [Route("~/api/punishmentsenter/{semafor}")]
        [HttpGet("{semafor}")]
        [Authorize]
        public async Task<IActionResult> GetPunishmentsenter([FromRoute] int semafor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = await _userManager.GetUserAsync(HttpContext.User);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            bool isUser = false;
            foreach (var t in roles)
            {
                if (t == "user") { isUser = true; break; }
                if (t == "admin") { isUser = false; break; }
            }

            List<Punishments> Temp = new List<Punishments>();

            if (isUser)
            {
                AutoOwners ao = new AutoOwners();
                _context.Punishments.Load();
                _context.AutoOwners.Load();

                foreach (var i in _context.AutoOwners)
                    if (user.Number == i.Number)
                    {
                        ao = i; break;
                    }

                foreach (var t in _context.Punishments)
                {
                    if (t.IdAowner == ao.Id)
                        Temp.Add(t);
                }
            }
            else
                foreach (var t in _context.Punishments)
                    Temp.Add(t);

            List<Punishments> punishments = new List<Punishments>();
            if(Temp != null)
            { 
                if (semafor == 1)
                    foreach(var temp in Temp)
                        if(temp.DatePay != null)
                            punishments.Add(temp);

                if (semafor == 2)
                    foreach (var temp in Temp)
                        if(temp.DatePay == null)
                            punishments.Add(temp);

                if (semafor != 1 && semafor != 2)
                    foreach (var temp in Temp)
                        punishments.Add(temp);
            }
            else
                return NotFound();
            /*if (punishments.Count == 0)
            {
                return Ok(null);
            }*/

            return Ok(punishments);
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

        //удаление записи
        // DELETE: api/Punishments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
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