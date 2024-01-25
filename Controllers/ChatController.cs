using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BerichtenOpties;
using Accounts;
using Microsoft.AspNetCore.Authorization;

namespace WDPR_i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ValidationController
    {
        private readonly WesselWestSideContext _context;

        public ChatController(WesselWestSideContext context) : base(context)
        {
            _context = context;
        }

        // GET: api/Chat
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChat()
        {
            Account user = GetUserFromJWT();

            if (_context.Chat == null)
            {
                return NotFound();
            }
            return _context.Chat;
            
            var filteredChats = _context.Chat
                .Where(chat => chat.Verzender.Id == user.Id || chat.Ontvanger.Id == user.Id)
                .ToListAsync();
            return await filteredChats;
            // return BadRequest(user.Id);
        }


        // GET: api/Chat/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(string id)
        {
            if (_context.Chat == null)
            {
                return NotFound();
            }
            
            var chat = await _context.Chat.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return chat;
        }

        // PUT: api/Chat/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(int id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
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

        // POST: api/Chat
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat([FromBody] ChatDto chatRequest)
        {
            Account user = GetUserFromJWT();
            user.getId()

            if (_context.Chat == null)
            {
                return Problem("Entity set 'WesselWestSideContext.Chat'  is null.");
            }
            // return Problem("Het is gelukt");
            Chat chat = new Chat
            {
                Verzender = user,
                Ontvanger = user,
                Tekst = chatRequest.Tekst
            };

            _context.Chat.Add(chat);
            // await _context.SaveChangesAsync();
            // return Ok(user);
            return CreatedAtAction("GetChat", new { id = chat.Id }, chat);
        }

        // DELETE: api/Chat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            if (_context.Chat == null)
            {
                return NotFound();
            }
            var chat = await _context.Chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chat.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatExists(int id)
        {
            return (_context.Chat?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
