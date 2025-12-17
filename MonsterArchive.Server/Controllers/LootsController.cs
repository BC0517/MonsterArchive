using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterArchive.Server.Data;
using MonsterArchive.Server.Data.Models;
using MonsterArchive.Server.DTO;

namespace MonsterArchive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LootsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LootsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Loots
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Loot>>> GetLoots()
        {
            return await _context.Loots.ToListAsync();
        }

        // GET: api/Loots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Loot>> GetLoot(int id)
        {
            var loot = await _context.Loots.FindAsync(id);

            if (loot == null)
            {
                return NotFound();
            }

            return loot;
        }

        // PUT: api/Loots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoot(int id, Loot loot)
        {
            if (id != loot.LootId)
            {
                return BadRequest();
            }

            _context.Entry(loot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LootExists(id))
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

        // POST: api/Loots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Loot>> PostLoot(LootDTO dto)
        {
            var loot = new Loot
            {
                ItemName = dto.ItemName,
                Rarity = dto.Rarity,
                MonsterId = dto.MonsterId
            };
            _context.Loots.Add(loot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoot", new { id = loot.LootId }, loot);
        }

        // DELETE: api/Loots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoot(int id)
        {
            var loot = await _context.Loots.FindAsync(id);
            if (loot == null)
            {
                return NotFound();
            }

            _context.Loots.Remove(loot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LootExists(int id)
        {
            return _context.Loots.Any(e => e.LootId == id);
        }
    }
}
