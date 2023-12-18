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
    public class BeheerderController : ControllerBase
    {
        private readonly BeheerderContext _context;

        public BeheerderController(BeheerderContext context)
        {
            _context = context;
        }

        // GET: api/Beheerder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beheerder>>> GetBeheerder()
        {
          if (_context.Beheerder == null)
          {
              return NotFound();
          }
            return await _context.Beheerder.ToListAsync();
        }

        // GET: api/Beheerder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beheerder>> GetBeheerder(int id)
        {
          if (_context.Beheerder == null)
          {
              return NotFound();
          }
            var beheerder = await _context.Beheerder.FindAsync(id);

            if (beheerder == null)
            {
                return NotFound();
            }

            return beheerder;
        }

        // PUT: api/Beheerder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeheerder(int id, Beheerder beheerder)
        {
            if (id != beheerder.Id)
            {
                return BadRequest();
            }

            _context.Entry(beheerder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeheerderExists(id))
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

        // POST: api/Beheerder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beheerder>> PostBeheerder(Beheerder beheerder)
        {
          if (_context.Beheerder == null)
          {
              return Problem("Entity set 'BeheerderContext.Beheerder'  is null.");
          }
            _context.Beheerder.Add(beheerder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeheerder", new { id = beheerder.Id }, beheerder);
        }

        // DELETE: api/Beheerder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeheerder(int id)
        {
            if (_context.Beheerder == null)
            {
                return NotFound();
            }
            var beheerder = await _context.Beheerder.FindAsync(id);
            if (beheerder == null)
            {
                return NotFound();
            }

            _context.Beheerder.Remove(beheerder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeheerderExists(int id)
        {
            return (_context.Beheerder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
