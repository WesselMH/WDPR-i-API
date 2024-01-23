using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Accounts;
using Microsoft.AspNetCore.Authorization;
using Onderzoeken;

namespace WDPR_i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErvaringsDeskundigeController : ValidationController
    {
        private readonly WesselWestSideContext _context;

        public ErvaringsDeskundigeController(WesselWestSideContext context) : base(context)
        {
            _context = context;
        }

        private ActionResult<IEnumerable<ErvaringsDeskundige>> CheckErvaringsDeskundigeSet()
        {
            if (_context.ErvaringsDeskundige == null)
            {
                return NotFound();
            }
            return null;
        }

        // GET: api/ErvaringsDeskundige
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErvaringsDeskundige>>> GetErvaringsDeskundige()
        {
            if (_context.ErvaringsDeskundige == null)
            {
                return NotFound();
            }
            return await _context.ErvaringsDeskundige.ToListAsync();
        }

        // GET: api/ErvaringsDeskundige/Onderzoeken
        [Authorize]
        [HttpGet]
        [Route("Onderzoeken")]
        public async Task<ActionResult<IEnumerable<Onderzoek>>> GetErvaringsDeskundigeOnderzoeken()
        {
            ErvaringsDeskundige user = (ErvaringsDeskundige)GetUserFromJWT();
            //user.id pakt id van Account, Die returnt niets
            //Na Account.Id eruit gecomment te hebben werkt het weer. maar dan kan je niet inloggen en registreren 
            //methode getId aangemaakt in Account. die returnt base.Id (van IdentityUser)
            // Console.WriteLine(user.Id);
            // Console.WriteLine(user.getId());


            if (_context.ErvaringsDeskundige == null)
            {
                return NotFound();
            }

            if (user.Onderzoeken == null)
            {
                return NotFound("Niet aangemeld voor een opdracht");
            }

            var getListOnderzoeken = _context.ErvaringsDeskundige.Where(e => e.Id == user.getId())
            .Include(e => e.Onderzoeken)
            .SelectMany(e => e.Onderzoeken)
            .Include(e => e.Uitvoerder)
            .ToListAsync();

            return await getListOnderzoeken;
        }

        // GET: api/ErvaringsDeskundige/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErvaringsDeskundige>> GetErvaringsDeskundige(string id)
        {
            if (_context.ErvaringsDeskundige == null)
            {
                return NotFound();
            }
            var ervaringsDeskundige = _context.ErvaringsDeskundige
            .Include(e => e.Voogd)
            .Include(e => e.BenaderOpties)
            .Include(e => e.Onderzoeken)
            .Include(e => e.Hulpmiddelen)
            .Include(e => e.Beperkingen)
            .Include(e => e.TypeOnderzoeken)
            .Where(e => e.Id == id)
            .FirstOrDefault();

            // var ervaringsDeskundige2 = _context.ErvaringsDeskundige.Include(e => (e.BenaderOpties)).Include(e => e.Onderzoeken).Include(e => e.Hulpmiddelen).Include(e => e.BenaderOpties).ToList();

            if (ervaringsDeskundige == null)
            {
                return NotFound();
            }

            return ervaringsDeskundige;
        }

        [Authorize]
        [HttpGet("user/getMyInfo")]
        public ActionResult<ErvaringsDeskundigeDto> GetMyInfo()
        {
            ErvaringsDeskundige user = (ErvaringsDeskundige)GetUserFromJWT();

            ErvaringsDeskundigeDto myInfo = new()
            {
                GebruikersNaam = user.GebruikersNaam,
                Email = user.EmailAccount,
                Voornaam = user.Voornaam,
                Achternaam = user.Achternaam
            };

            return Ok(myInfo);
        }

        // PUT: api/ErvaringsDeskundige/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErvaringsDeskundige(string id, ErvaringsDeskundige ervaringsDeskundige)
        {
            if (id != ervaringsDeskundige.Id)
            {
                return BadRequest();
            }

            _context.Entry(ervaringsDeskundige).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErvaringsDeskundigeExists(id))
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

        // POST: api/ErvaringsDeskundige
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErvaringsDeskundige>> PostErvaringsDeskundige(ErvaringsDeskundige ervaringsDeskundige)
        {
            if (_context.ErvaringsDeskundige == null)
            {
                return Problem("Entity set 'WesselWestSideContext.ErvaringsDeskundige'  is null.");
            }
            _context.ErvaringsDeskundige.Add(ervaringsDeskundige);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ErvaringsDeskundigeExists(ervaringsDeskundige.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetErvaringsDeskundige", new { id = ervaringsDeskundige.Id }, ervaringsDeskundige);
        }

        // DELETE: api/ErvaringsDeskundige/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErvaringsDeskundige(string id)
        {
            if (_context.ErvaringsDeskundige == null)
            {
                return NotFound();
            }
            var ervaringsDeskundige = await _context.ErvaringsDeskundige.FindAsync(id);
            if (ervaringsDeskundige == null)
            {
                return NotFound();
            }

            _context.ErvaringsDeskundige.Remove(ervaringsDeskundige);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [Authorize]
        [HttpPut("AddOnderzoek/{id}")]
        public async Task<ActionResult<ErvaringsDeskundige>> PostAddOnderzoek(int id)
        {
            if (_context.ErvaringsDeskundige == null)
            {
                return Problem("Entity set 'WesselWestSideContext.ErvaringsDeskundige'  is null.");
            }

            ErvaringsDeskundige user = (ErvaringsDeskundige)GetUserFromJWT();

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.Onderzoeken == null)
            {
                user.Onderzoeken = new List<Onderzoek>();
            }

            Onderzoek? opdracht = _context.Onderzoek.SingleOrDefault(x => x.Id == id);

            if (opdracht == null)
            {
                return BadRequest("Opdracht niet in database");
            }


            user.Onderzoeken.Add(opdracht);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict("Gebruiker al toegevoegd aan onderzoek");
            }
            return CreatedAtAction("GetErvaringsDeskundige", new { id = user.Id }, user);
        }

        private bool ErvaringsDeskundigeExists(string id)
        {
            return (_context.ErvaringsDeskundige?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
