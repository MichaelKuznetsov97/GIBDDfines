using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GIBDDfines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GIBDDfines.Controllers
{
    //Контроллер для загрузки данных на форму создания нарушения
    public class CreateController : Controller
    {
        private readonly modeldbGIBDD2Context _context;

        public CreateController(modeldbGIBDD2Context context)
        {
            _context = context;
        }

        [Route("api/create/onloadOwners")]
        [HttpGet]
        public IActionResult OnloadOwners()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AutoOwners.Load();
            var aowners = _context.AutoOwners;

            if (aowners == null)
                return NotFound();

            return Ok(aowners);
        }

        [Route("api/create/onloadAutoes")]
        [HttpGet]
        public IActionResult OnloadAutoes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Autoes.Load();
            var autoes = _context.Autoes
                .Include(auto=>auto.IdMarkModelNavigation)
                    .ThenInclude(model=>model.IdMarkNavigation);

            if (autoes == null)
                return NotFound();

            return Ok(autoes);
        }

        [Route("api/create/onloadPolice")]
        [HttpGet]
        public IActionResult OnloadPolice()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Police.Load();
            var police = _context.Police
                .Include(pol => pol.IdTitleNavigation);

            if (police == null)
                return NotFound();

            return Ok(police);
        }

        [Route("api/create/onloadTPun")]
        [HttpGet]
        public IActionResult OnloadTPun()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TypePunishments.Load();
            var tpun = _context.TypePunishments;

            if (tpun == null)
                return NotFound();

            return Ok(tpun);
        }
    }
}
