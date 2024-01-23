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
    public class OnderzoekController : ControllerBase
    {
        private readonly WesselWestSideContext _context;

        public OnderzoekController(WesselWestSideContext context)
        {
            _context = context;
        }

        // public static readonly List<Onderzoek> listOnderzoek = new List<Onderzoek>
        // {
        //     new Onderzoek{Id=0, Titel="Toegankelijkheid voor Mensen met Beperkingen",Beschrijving="Studie naar Kwaliteit van Leven bij Mensen met Beperkingen: Een diepgaand onderzoek naar de dagelijkse hindernissen en mogelijkheden tot verbetering. Het doel is waardevolle inzichten te vergaren ter bevordering van levenskwaliteit en inclusie voor deze individuen.",Locatie="", Status="true",Beloning="",Uitvoerder=new Accounts.Bedrijf{GebruikersNaam = "Accessibility Foundation"}, Datum = new DateTime(2023,12,12)},
        //     new Onderzoek{Id=1, Titel="Inclusie en Welzijn bij Mensen met Beperkingen",Beschrijving="Onderzoek naar Levenskwaliteit bij Personen met een Beperking: Een analyse van sociale inclusie en dagelijkse uitdagingen, met het doel bij te dragen aan verbeterde maatschappelijke participatie en welzijn.",Locatie="", Status="true",Beloning="",Uitvoerder=new Accounts.Bedrijf{GebruikersNaam = "Mediamarkt"}, Datum = new DateTime(2023,12,12)},
        //     new Onderzoek{Id=2, Titel="Toegankelijkheidsbeoordeling 2023",Beschrijving="Deze enquête is ontworpen om inzicht te krijgen in uw ervaringen met de toegankelijkheid van onze producten/diensten/ruimtes. Neem alstublieft de tijd om de vragen eerlijk en gedetailleerd te beantwoorden. Uw input zal direct bijdragen aan het verbeteren van onze inspanningen op het gebied van inclusiviteit.",Locatie="", Status="false",Beloning="",Uitvoerder=new Accounts.Bedrijf{GebruikersNaam = "Bartiméus"}, Datum = new DateTime(2023,12,12)}
        // };

        // GET: api/Onderzoek
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Onderzoek>>> GetOnderzoek()
        {
          if (_context.Onderzoek == null)
          {
              return NotFound();
          }
            return await _context.Onderzoek.Include(x => x.Uitvoerder).ToListAsync();
        }

        // GET: api/Onderzoek/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Onderzoek>> GetOnderzoek(int id)
        {
          if (_context.Onderzoek == null)
          {
              return NotFound();
          }
            var onderzoek = await _context.Onderzoek.Include(x => x.Uitvoerder).Include(x=> x.SoortOnderzoek).SingleOrDefaultAsync(x => x.Id == id);

            if (onderzoek == null)
            {
                return NotFound();
            }

            return onderzoek;
        }

        // PUT: api/Onderzoek/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOnderzoek(int id, Onderzoek onderzoek)
        {
            if (id != onderzoek.Id)
            {
                return BadRequest();
            }

            _context.Entry(onderzoek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OnderzoekExists(id))
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

        // POST: api/Onderzoek
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Onderzoek>> PostOnderzoek(Onderzoek onderzoek)
        {
          if (_context.Onderzoek == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Onderzoek'  is null.");
          }
            _context.Onderzoek.Add(onderzoek);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OnderzoekExists(onderzoek.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOnderzoek", new { id = onderzoek.Id }, onderzoek);
        }

        [HttpPost]
        [Route("AddDeskundige")]
        public async Task<ActionResult<Onderzoek>> PostAddDeskundige(int id, [FromBody]Onderzoek onderzoek)
        {
          if (_context.Onderzoek == null)
          {
              return Problem("Entity set 'WesselWestSideContext.Onderzoek'  is null.");
          }

            _context.Onderzoek.Add(onderzoek);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OnderzoekExists(onderzoek.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOnderzoek", new { id = onderzoek.Id }, onderzoek);
        }

        // DELETE: api/Onderzoek/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOnderzoek(int id)
        {
            if (_context.Onderzoek == null)
            {
                return NotFound();
            }
            var onderzoek = await _context.Onderzoek.FindAsync(id);
            if (onderzoek == null)
            {
                return NotFound();
            }

            _context.Onderzoek.Remove(onderzoek);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OnderzoekExists(int id)
        {
            return (_context.Onderzoek?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
