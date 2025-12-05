using System.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using MonsterArchive.Server.Data;
using MonsterArchive.Server.Data.Models;

namespace MonsterArchive.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            // only allow in Development
            if (!_env.IsDevelopment())
                throw new SecurityException("Not allowed");

            var path = Path.Combine(
                _env.ContentRootPath,
                "Data/Source/COMP 584 Monster Data.xlsx");

            using var stream = System.IO.File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            var worksheet = excelPackage.Workbook.Worksheets[0];
            var nEndRow = worksheet.Dimension.End.Row;

            int numberOfMonstersAdded = 0;
            int numberOfLootAdded = 0;

            // lookup dictionary of existing monsters
            var monstersByName = _context.Monsters
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            // === First pass: add Monsters ===
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var monsterName = worksheet.Cells[nRow, 1].GetValue<string>();
                var species = worksheet.Cells[nRow, 2].GetValue<string>();
                var element = worksheet.Cells[nRow, 3].GetValue<string>();
                var weakness = worksheet.Cells[nRow, 5].GetValue<string>();
                var rank = worksheet.Cells[nRow, 6].GetValue<string>();
                var aggressionLevel = worksheet.Cells[nRow, 7].GetValue<string>();

                if (string.IsNullOrWhiteSpace(monsterName))
                    continue;

                if (monstersByName.ContainsKey(monsterName))
                    continue;

                var monster = new Monster
                {
                    Name = monsterName,
                    Species = species,
                    Element = element,
                    Weakness = weakness,
                    Rank = rank,
                    AggressionLevel = aggressionLevel
                };

                await _context.Monsters.AddAsync(monster);
                monstersByName.Add(monsterName, monster);
                numberOfMonstersAdded++;
            }

            if (numberOfMonstersAdded > 0)
                await _context.SaveChangesAsync();

            // refresh lookup with IDs now assigned
            monstersByName = _context.Monsters
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            // === Second pass: add Loot ===
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var monsterName = worksheet.Cells[nRow, 1].GetValue<string>();
                var itemName = worksheet.Cells[nRow, 9].GetValue<string>();
                var rarity = worksheet.Cells[nRow, 10].GetValue<string>();

                if (string.IsNullOrWhiteSpace(monsterName) || string.IsNullOrWhiteSpace(itemName))
                    continue;

                if (!monstersByName.ContainsKey(monsterName))
                    continue;

                var monsterId = monstersByName[monsterName].Id;

                if (!_context.Loots.Any(l => l.ItemName == itemName && l.Rarity == rarity && l.MonsterId == monsterId))
                {
                    var loot = new Loot
                    {
                        ItemName = itemName,
                        Rarity = rarity,
                        MonsterId = monsterId
                    };
                    _context.Loots.Add(loot);
                    numberOfLootAdded++;
                }
            }

            if (numberOfLootAdded > 0)
                await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                MonstersAdded = numberOfMonstersAdded,
                LootAdded = numberOfLootAdded
            });
        }
    }
}