using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WDPR_i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HulpmiddelenController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public HulpmiddelenController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/Hulpmiddelen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hulpmiddelen>>> GetHulpmiddelen()
        {
          if (_context.Hulpmiddelen == null)
          {
              return NotFound();
          }
            return await _context.Hulpmiddelen.ToListAsync();
        }

        // GET: api/Hulpmiddelen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hulpmiddelen>> GetHulpmiddelen(int id)
        {
          if (_context.Hulpmiddelen == null)
          {
              return NotFound();
          }
            var hulpmiddelen = await _context.Hulpmiddelen.FindAsync(id);

            if (hulpmiddelen == null)
            {
                return NotFound();
            }

            return hulpmiddelen;
        }

        // PUT: api/Hulpmiddelen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHulpmiddelen(int id, Hulpmiddelen hulpmiddelen)
        {
            if (id != hulpmiddelen.Id)
            {
                return BadRequest();
            }

            _context.Entry(hulpmiddelen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HulpmiddelenExists(id))
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

        // POST: api/Hulpmiddelen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hulpmiddelen>> PostHulpmiddelen(Hulpmiddelen hulpmiddelen)
        {
          if (_context.Hulpmiddelen == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Hulpmiddelen'  is null.");
          }
            _context.Hulpmiddelen.Add(hulpmiddelen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHulpmiddelen", new { id = hulpmiddelen.Id }, hulpmiddelen);
        }

        // DELETE: api/Hulpmiddelen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHulpmiddelen(int id)
        {
            if (_context.Hulpmiddelen == null)
            {
                return NotFound();
            }
            var hulpmiddelen = await _context.Hulpmiddelen.FindAsync(id);
            if (hulpmiddelen == null)
            {
                return NotFound();
            }

            _context.Hulpmiddelen.Remove(hulpmiddelen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HulpmiddelenExists(int id)
        {
            return (_context.Hulpmiddelen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
