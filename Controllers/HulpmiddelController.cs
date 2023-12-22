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
    public class HulpmiddelController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public HulpmiddelController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/Hulpmiddel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hulpmiddel>>> GetHulpmiddel()
        {
          if (_context.Hulpmiddel == null)
          {
              return NotFound();
          }
            return await _context.Hulpmiddel.ToListAsync();
        }

        // GET: api/Hulpmiddel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hulpmiddel>> GetHulpmiddel(int id)
        {
          if (_context.Hulpmiddel == null)
          {
              return NotFound();
          }
            var hulpmiddel = await _context.Hulpmiddel.FindAsync(id);

            if (hulpmiddel == null)
            {
                return NotFound();
            }

            return hulpmiddel;
        }

        // PUT: api/Hulpmiddel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHulpmiddel(int id, Hulpmiddel hulpmiddel)
        {
            if (id != hulpmiddel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hulpmiddel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HulpmiddelExists(id))
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

        // POST: api/Hulpmiddel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hulpmiddel>> PostHulpmiddel(Hulpmiddel hulpmiddel)
        {
          if (_context.Hulpmiddel == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Hulpmiddel'  is null.");
          }
            _context.Hulpmiddel.Add(hulpmiddel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHulpmiddel", new { id = hulpmiddel.Id }, hulpmiddel);
        }

        // DELETE: api/Hulpmiddel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHulpmiddel(int id)
        {
            if (_context.Hulpmiddel == null)
            {
                return NotFound();
            }
            var hulpmiddel = await _context.Hulpmiddel.FindAsync(id);
            if (hulpmiddel == null)
            {
                return NotFound();
            }

            _context.Hulpmiddel.Remove(hulpmiddel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HulpmiddelExists(int id)
        {
            return (_context.Hulpmiddel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
