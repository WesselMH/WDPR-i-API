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
    public class BenaderOptieController : ValidationController
    {
        private readonly WesselWestSideContext _context;

        public BenaderOptieController(WesselWestSideContext context) : base(context)
        {
            _context = context;
        }

        // GET: api/BenaderOptie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenaderOptie>>> GetBenaderOptie()
        {
            if (_context.BenaderOptie == null)
            {
                return NotFound();
            }
            return await _context.BenaderOptie.ToListAsync();
        }

        // GET: api/BenaderOptie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BenaderOptie>> GetBenaderOptie(string id)
        {
            if (_context.BenaderOptie == null)
            {
                return NotFound();
            }
            var benaderOptie = await _context.BenaderOptie.FindAsync(id);

            if (benaderOptie == null)
            {
                return NotFound();
            }

            return benaderOptie;
        }

        // PUT: api/BenaderOptie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenaderOptie(int id, BenaderOptie benaderOptie)
        {
            if (id != benaderOptie.Id)
            {
                return BadRequest();
            }

            _context.Entry(benaderOptie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BenaderOptieExists(id))
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

        // POST: api/BenaderOptie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BenaderOptie>> PostBenaderOptie(BenaderOptie benaderOptie)
        {
            if (_context.BenaderOptie == null)
            {
                return Problem("Entity set 'WesselWestSideContext.BenaderOptie'  is null.");
            }
            _context.BenaderOptie.Add(benaderOptie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BenaderOptieExists(benaderOptie.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBenaderOptie", new { id = benaderOptie.Id }, benaderOptie);
        }

        // DELETE: api/BenaderOptie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBenaderOptie(int id)
        {
            if (_context.BenaderOptie == null)
            {
                return NotFound();
            }
            var benaderOptie = await _context.BenaderOptie.FindAsync(id);
            if (benaderOptie == null)
            {
                return NotFound();
            }

            _context.BenaderOptie.Remove(benaderOptie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BenaderOptieExists(int id)
        {
            return (_context.BenaderOptie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
