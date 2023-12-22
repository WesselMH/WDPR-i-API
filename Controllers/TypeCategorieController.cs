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
    public class TypeCategorieController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public TypeCategorieController(WesselWestSideContext context)
        {
            _context = context;
        }

        // GET: api/TypeCategorie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeCategorie>>> GetTypeCategorie()
        {
          if (_context.TypeCategorie == null)
          {
              return NotFound();
          }
            return await _context.TypeCategorie.ToListAsync();
        }

        // GET: api/TypeCategorie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeCategorie>> GetTypeCategorie(int id)
        {
          if (_context.TypeCategorie == null)
          {
              return NotFound();
          }
            var typeCategorie = await _context.TypeCategorie.FindAsync(id);

            if (typeCategorie == null)
            {
                return NotFound();
            }

            return typeCategorie;
        }

        // PUT: api/TypeCategorie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeCategorie(int id, TypeCategorie typeCategorie)
        {
            if (id != typeCategorie.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeCategorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeCategorieExists(id))
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

        // POST: api/TypeCategorie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeCategorie>> PostTypeCategorie(TypeCategorie typeCategorie)
        {
          if (_context.TypeCategorie == null)
          {
              return Problem("Entity set 'WesselWestSideContext.TypeCategorie'  is null.");
          }
            _context.TypeCategorie.Add(typeCategorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeCategorie", new { id = typeCategorie.Id }, typeCategorie);
        }

        // DELETE: api/TypeCategorie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeCategorie(int id)
        {
            if (_context.TypeCategorie == null)
            {
                return NotFound();
            }
            var typeCategorie = await _context.TypeCategorie.FindAsync(id);
            if (typeCategorie == null)
            {
                return NotFound();
            }

            _context.TypeCategorie.Remove(typeCategorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeCategorieExists(int id)
        {
            return (_context.TypeCategorie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
