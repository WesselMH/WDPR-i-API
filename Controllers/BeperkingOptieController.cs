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
    public class BeperkingOptieController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public BeperkingOptieController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/BeperkingOptie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeperkingOptie>>> GetBeperkingOptie()
        {
          if (_context.BeperkingOptie == null)
          {
              return NotFound();
          }
            return await _context.BeperkingOptie.ToListAsync();
        }

        // GET: api/BeperkingOptie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BeperkingOptie>> GetBeperkingOptie(string id)
        {
          if (_context.BeperkingOptie == null)
          {
              return NotFound();
          }
            var beperkingOptie = await _context.BeperkingOptie.FindAsync(id);

            if (beperkingOptie == null)
            {
                return NotFound();
            }

            return beperkingOptie;
        }

        // PUT: api/BeperkingOptie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeperkingOptie(int id, BeperkingOptie beperkingOptie)
        {
            if (id != beperkingOptie.Id)
            {
                return BadRequest();
            }

            _context.Entry(beperkingOptie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeperkingOptieExists(id))
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

        // POST: api/BeperkingOptie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BeperkingOptie>> PostBeperkingOptie(BeperkingOptie beperkingOptie)
        {
          if (_context.BeperkingOptie == null)
          {
              return Problem("Entity set 'WesselWestSideContext.BeperkingOptie'  is null.");
          }
            _context.BeperkingOptie.Add(beperkingOptie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BeperkingOptieExists(beperkingOptie.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBeperkingOptie", new { id = beperkingOptie.Id }, beperkingOptie);
        }

        // DELETE: api/BeperkingOptie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeperkingOptie(int id)
        {
            if (_context.BeperkingOptie == null)
            {
                return NotFound();
            }
            var beperkingOptie = await _context.BeperkingOptie.FindAsync(id);
            if (beperkingOptie == null)
            {
                return NotFound();
            }

            _context.BeperkingOptie.Remove(beperkingOptie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeperkingOptieExists(int id)
        {
            return (_context.BeperkingOptie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
