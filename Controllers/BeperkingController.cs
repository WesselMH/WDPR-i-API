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
    public class BeperkingController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public BeperkingController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/Beperking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beperking>>> GetBeperking()
        {
          if (_context.Beperking == null)
          {
              return NotFound();
          }
            return await _context.Beperking.ToListAsync();
        }

        // GET: api/Beperking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beperking>> GetBeperking(int id)
        {
          if (_context.Beperking == null)
          {
              return NotFound();
          }
            var beperking = await _context.Beperking.FindAsync(id);

            if (beperking == null)
            {
                return NotFound();
            }

            return beperking;
        }

        // PUT: api/Beperking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeperking(int id, Beperking beperking)
        {
            if (id != beperking.Id)
            {
                return BadRequest();
            }

            _context.Entry(beperking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeperkingExists(id))
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

        // POST: api/Beperking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beperking>> PostBeperking(Beperking beperking)
        {
          if (_context.Beperking == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Beperking'  is null.");
          }
            _context.Beperking.Add(beperking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeperking", new { id = beperking.Id }, beperking);
        }

        // DELETE: api/Beperking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeperking(int id)
        {
            if (_context.Beperking == null)
            {
                return NotFound();
            }
            var beperking = await _context.Beperking.FindAsync(id);
            if (beperking == null)
            {
                return NotFound();
            }

            _context.Beperking.Remove(beperking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeperkingExists(int id)
        {
            return (_context.Beperking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
