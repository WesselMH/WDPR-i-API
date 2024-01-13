using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Accounts;

namespace WDPR_i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeschikbaarheidController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public BeschikbaarheidController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/Beschikbaarheid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beschikbaarheid>>> GetBeschikbaarheid()
        {
          if (_context.Beschikbaarheid == null)
          {
              return NotFound();
          }
            return await _context.Beschikbaarheid.ToListAsync();
        }

        // GET: api/Beschikbaarheid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beschikbaarheid>> GetBeschikbaarheid(string id)
        {
          if (_context.Beschikbaarheid == null)
          {
              return NotFound();
          }
            var beschikbaarheid = await _context.Beschikbaarheid.FindAsync(id);

            if (beschikbaarheid == null)
            {
                return NotFound();
            }

            return beschikbaarheid;
        }

        // PUT: api/Beschikbaarheid/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeschikbaarheid(string id, Beschikbaarheid beschikbaarheid)
        {
            if (id != beschikbaarheid.Id)
            {
                return BadRequest();
            }

            _context.Entry(beschikbaarheid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeschikbaarheidExists(id))
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

        // POST: api/Beschikbaarheid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beschikbaarheid>> PostBeschikbaarheid(Beschikbaarheid beschikbaarheid)
        {
          if (_context.Beschikbaarheid == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Beschikbaarheid'  is null.");
          }
            _context.Beschikbaarheid.Add(beschikbaarheid);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BeschikbaarheidExists(beschikbaarheid.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBeschikbaarheid", new { id = beschikbaarheid.Id }, beschikbaarheid);
        }

        // DELETE: api/Beschikbaarheid/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeschikbaarheid(string id)
        {
            if (_context.Beschikbaarheid == null)
            {
                return NotFound();
            }
            var beschikbaarheid = await _context.Beschikbaarheid.FindAsync(id);
            if (beschikbaarheid == null)
            {
                return NotFound();
            }

            _context.Beschikbaarheid.Remove(beschikbaarheid);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeschikbaarheidExists(string id)
        {
            return (_context.Beschikbaarheid?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
