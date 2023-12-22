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
    public class BenaderingController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public BenaderingController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/Benadering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Benadering>>> GetBenadering()
        {
          if (_context.Benadering == null)
          {
              return NotFound();
          }
            return await _context.Benadering.ToListAsync();
        }

        // GET: api/Benadering/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Benadering>> GetBenadering(int id)
        {
          if (_context.Benadering == null)
          {
              return NotFound();
          }
            var benadering = await _context.Benadering.FindAsync(id);

            if (benadering == null)
            {
                return NotFound();
            }

            return benadering;
        }

        // PUT: api/Benadering/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenadering(int id, Benadering benadering)
        {
            if (id != benadering.Id)
            {
                return BadRequest();
            }

            _context.Entry(benadering).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BenaderingExists(id))
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

        // POST: api/Benadering
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Benadering>> PostBenadering(Benadering benadering)
        {
          if (_context.Benadering == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Benadering'  is null.");
          }
            _context.Benadering.Add(benadering);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBenadering", new { id = benadering.Id }, benadering);
        }

        // DELETE: api/Benadering/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBenadering(int id)
        {
            if (_context.Benadering == null)
            {
                return NotFound();
            }
            var benadering = await _context.Benadering.FindAsync(id);
            if (benadering == null)
            {
                return NotFound();
            }

            _context.Benadering.Remove(benadering);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BenaderingExists(int id)
        {
            return (_context.Benadering?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
