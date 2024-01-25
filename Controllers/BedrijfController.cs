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
    [Route("api/[controller]")]
    [ApiController]
    public class BedrijfController : ValidationController
    {
        private readonly WesselWestSideContext _context;

        public BedrijfController(WesselWestSideContext context) : base(context)
        {
            _context = context;
        }


        // GET: api/Bedrijf
        [Authorize(Roles = "beheerder")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bedrijf>>> GetBedrijf()
        {
            if (_context.Bedrijf == null)
            {
                return NotFound();
            }
            return await _context.Bedrijf.ToListAsync();
        }

        // GET: api/Bedrijf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bedrijf>> GetBedrijf(string id)
        {
            if (_context.Bedrijf == null)
            {
                return NotFound();
            }
            var bedrijf = await _context.Bedrijf.FindAsync(id);

            if (bedrijf == null)
            {
                return NotFound();
            }

            return bedrijf;
        }

        // PUT: api/Bedrijf/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBedrijf(string id, Bedrijf bedrijf)
        {
            if (id != bedrijf.Id)
            {
                return BadRequest();
            }

            _context.Entry(bedrijf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BedrijfExists(id))
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

        // POST: api/Bedrijf
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bedrijf>> PostBedrijf(Bedrijf bedrijf)
        {
            if (_context.Bedrijf == null)
            {
                return Problem("Entity set 'WesselWestSideContext.Bedrijf'  is null.");
            }
            _context.Bedrijf.Add(bedrijf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBedrijf", new { id = bedrijf.Id }, bedrijf);
        }

        // DELETE: api/Bedrijf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBedrijf(string id)
        {
            if (_context.Bedrijf == null)
            {
                return NotFound();
            }
            var bedrijf = await _context.Bedrijf.FindAsync(id);
            if (bedrijf == null)
            {
                return NotFound();
            }

            _context.Bedrijf.Remove(bedrijf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BedrijfExists(string id)
        {
            return (_context.Bedrijf?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
