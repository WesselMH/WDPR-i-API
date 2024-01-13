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
    public class VoogdController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public VoogdController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/Voogd
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voogd>>> GetVoogd()
        {
          if (_context.Voogd == null)
          {
              return NotFound();
          }
            return await _context.Voogd.ToListAsync();
        }

        // GET: api/Voogd/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voogd>> GetVoogd(string id)
        {
          if (_context.Voogd == null)
          {
              return NotFound();
          }
            var voogd = await _context.Voogd.FindAsync(id);

            if (voogd == null)
            {
                return NotFound();
            }

            return voogd;
        }

        // PUT: api/Voogd/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoogd(string id, Voogd voogd)
        {
            if (id != voogd.Id)
            {
                return BadRequest();
            }

            _context.Entry(voogd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoogdExists(id))
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

        // POST: api/Voogd
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voogd>> PostVoogd(Voogd voogd)
        {
          if (_context.Voogd == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Voogd'  is null.");
          }
            _context.Voogd.Add(voogd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoogd", new { id = voogd.Id }, voogd);
        }

        // DELETE: api/Voogd/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoogd(string id)
        {
            if (_context.Voogd == null)
            {
                return NotFound();
            }
            var voogd = await _context.Voogd.FindAsync(id);
            if (voogd == null)
            {
                return NotFound();
            }

            _context.Voogd.Remove(voogd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoogdExists(string id)
        {
            return (_context.Voogd?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
