using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Accounts;
using Microsoft.AspNetCore.Authorization;

namespace WDPR_i_API.Controllers
{
    [Authorize(Roles = "beheerder, ervaringsDeskundige")]
    [Route("api/[controller]")]
    [ApiController]
    public class HulpmiddelController : ValidationController
    {
        private readonly WesselWestSideContext _context;

        public HulpmiddelController(WesselWestSideContext context) : base(context)
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
        [Authorize(Roles = "beheerder")]
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
        [Authorize(Roles = "beheerder")]
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
        [Authorize(Roles = "beheerder")]
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
