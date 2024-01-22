using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Accounts;
using Onderzoeken;


namespace WDPR_i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErvaringsDeskundigeController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public ErvaringsDeskundigeController(WesselWestSideContext context)
        {
            _context = context;
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
        [HttpGet]
        [Route("Onderzoeken")]
        public async Task<ActionResult<IEnumerable<Onderzoek>>> GetErvaringsDeskundigeOnderzoeken(string id)
        {
            if (_context.ErvaringsDeskundige == null)
            {
                return NotFound();
            }
            var getListOnderzoeken = _context.ErvaringsDeskundige.Where(e => e.Id == id)
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

        [HttpPost]
        [Route("AddOnderzoek/{id}")]
        public async Task<ActionResult<ErvaringsDeskundige>> PostAddOnderzoek([FromRoute]int id,[FromBody] ErvaringsDeskundige ervaringsDeskundige)
        {
          if (_context.ErvaringsDeskundige == null)
          {
              return Problem("Entity set 'WesselWestSideContext.ErvaringsDeskundige'  is null.");
          }
            string ervaringsDeskundigeId = ervaringsDeskundige.Id;
            var deskundige = _context.ErvaringsDeskundige.Single((x) => x.Id == ervaringsDeskundigeId);
            var onderzoek = _context.Onderzoek.Single((x) => x.Id == id);
            
            if (deskundige.Onderzoeken == null)
            {
                deskundige.Onderzoeken = new List<Onderzoeken.Onderzoek>();
            }
            
            deskundige.Onderzoeken.Add(onderzoek);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ErvaringsDeskundigeExists(ervaringsDeskundige.Id))
                {
                    return Conflict("Gebruiker al toegevoegd aan onderzoek");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetErvaringsDeskundige", new { id = ervaringsDeskundige.Id }, ervaringsDeskundige);
        }

        private bool ErvaringsDeskundigeExists(string id)
        {
            return (_context.ErvaringsDeskundige?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
