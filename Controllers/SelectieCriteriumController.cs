using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onderzoeken;

namespace WDPR_i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectieCriteriumController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public SelectieCriteriumController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/SelectieCriterium
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectieCriterium>>> GetSelectieCriterium()
        {
            if (_context.SelectieCriterium == null)
            {
                return NotFound();
            }
            return await _context.SelectieCriterium.ToListAsync();
        }

        // GET: api/SelectieCriterium/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SelectieCriterium>> GetSelectieCriterium(string id)
        {
            if (_context.SelectieCriterium == null)
            {
                return NotFound();
            }
            var selectieCriterium = await _context.SelectieCriterium.FindAsync(id);

            if (selectieCriterium == null)
            {
                return NotFound();
            }

            return selectieCriterium;
        }

        // PUT: api/SelectieCriterium/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSelectieCriterium(string id, SelectieCriterium selectieCriterium)
        {
            if (id != selectieCriterium.Id)
            {
                return BadRequest();
            }

            _context.Entry(selectieCriterium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectieCriteriumExists(id))
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

        // POST: api/SelectieCriterium
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SelectieCriterium>> PostSelectieCriterium(SelectieCriterium selectieCriterium)
        {
            if (_context.SelectieCriterium == null)
            {
                return Problem("Entity set 'WesselWestSideContext.SelectieCriterium'  is null.");
            }
            _context.SelectieCriterium.Add(selectieCriterium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSelectieCriterium", new { id = selectieCriterium.Id }, selectieCriterium);
        }

        // DELETE: api/SelectieCriterium/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSelectieCriterium(string id)
        {
            if (_context.SelectieCriterium == null)
            {
                return NotFound();
            }
            var selectieCriterium = await _context.SelectieCriterium.FindAsync(id);
            if (selectieCriterium == null)
            {
                return NotFound();
            }

            _context.SelectieCriterium.Remove(selectieCriterium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SelectieCriteriumExists(string id)
        {
            return (_context.SelectieCriterium?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
