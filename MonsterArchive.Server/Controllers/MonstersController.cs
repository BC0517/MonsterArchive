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
    public class MonstersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MonstersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Monsters
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monster>>> GetMonsters()
        {
            return await _context.Monsters.ToListAsync();
        }

        // GET: api/Monsters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonsterDetailsDTO>> GetMonster(int id)
        {
            var monster = await _context.Monsters
                .Where(m => m.MonsterId == id)
                .Select(m => new MonsterDetailsDTO
                {
                    MonsterId = m.MonsterId,
                    Name = m.Name,
                    Species = m.Species,
                    Element = m.Element,
                    Weakness = m.Weakness,
                    Rank = m.Rank,
                    AggressionLevel = m.AggressionLevel,
                    Loots = m.Loots.Select(l => new LootDTO
                    {
                        ItemName = l.ItemName,
                        Rarity = l.Rarity,
                        MonsterId = l.MonsterId
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (monster == null)
                return NotFound();

            return Ok(monster);
        }


        // PUT: api/Monsters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonster(int id, Monster monster)
        {
            if (id != monster.MonsterId)
            {
                return BadRequest();
            }

            _context.Entry(monster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonsterExists(id))
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

        // POST: api/Monsters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monster>> PostMonster(MonsterDTO dto)
        {
            var monster = new Monster
            {
                MonsterId = dto.MonsterId,
                Name = dto.Name,
                Species = dto.Species,
                Element = dto.Element,
                Weakness = dto.Weakness,
                Rank = dto.Rank,
                AggressionLevel = dto.AggressionLevel
            };

            _context.Monsters.Add(monster);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonsterExists(monster.MonsterId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonster", new { id = monster.MonsterId }, monster);
        }

        // DELETE: api/Monsters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonster(int id)
        {
            var monster = await _context.Monsters.FindAsync(id);
            if (monster == null)
            {
                return NotFound();
            }

            _context.Monsters.Remove(monster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonsterExists(int id)
        {
            return _context.Monsters.Any(e => e.MonsterId == id);
        }
    }
}
