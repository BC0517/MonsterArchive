using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterArchive.Server.Data;
using MonsterArchive.Server.Data.Models;
using MonsterArchive.Server.DTO;
using OfficeOpenXml;
using System.Security;
using RegisterRequest = MonsterArchive.Server.DTO.RegisterRequest;

namespace MonsterArchive.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController(ApplicationDbContext context, IWebHostEnvironment env,IConfiguration configuration,
        RoleManager<IdentityRole> roleManager, UserManager<MonsterArchiveUser> userManager) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IWebHostEnvironment _env = env;


        // POST: api/Seed/import, don't use GET
        [HttpPost("import")]
        public async Task<IActionResult> Import()
        {
            //// Restrict access to development environment only
            //if (!_env.IsDevelopment())
            //    return Forbid();

            // Read the Excel file from the specified path
            var path = Path.Combine(
                _env.ContentRootPath,
                "Data/Source/COMP 584 Monster Data.xlsx");

            if (!System.IO.File.Exists(path))
                return NotFound("Seed file not found.");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var stream = System.IO.File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            var worksheet = excelPackage.Workbook.Worksheets.First();
            var endRow = worksheet.Dimension.End.Row;

            int monstersAdded = 0;
            int lootAdded = 0;

            // Load existing monsters and loot into memory for quick lookup
            var monstersById = await _context.Monsters
                .AsNoTracking()
                .ToDictionaryAsync(m => m.MonsterId);

            var existingLoot = await _context.Loots
                .AsNoTracking()
                .Select(l => new { l.ItemName, l.Rarity, l.MonsterId })
                .ToListAsync();

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // PASS Monsters
                for (int row = 2; row <= endRow; row++)
                {
                    var monsterId = worksheet.Cells[row, 1].GetValue<int>();
                    var name = worksheet.Cells[row, 2].GetValue<string>();

                    if (monsterId == 0 || string.IsNullOrWhiteSpace(name))
                        continue;

                    if (monstersById.ContainsKey(monsterId))
                        continue;

                    var monster = new Monster
                    {
                        MonsterId = monsterId, // external ID — intentional
                        Name = name,
                        Species = worksheet.Cells[row, 3].GetValue<string>(),
                        Element = worksheet.Cells[row, 4].GetValue<string>(),
                        Weakness = worksheet.Cells[row, 5].GetValue<string>(),
                        Rank = worksheet.Cells[row, 6].GetValue<string>(),
                        AggressionLevel = worksheet.Cells[row, 7].GetValue<string>()
                    };

                    _context.Monsters.Add(monster);
                    monstersById.Add(monsterId, monster);
                    monstersAdded++;
                }

                if (monstersAdded > 0)
                    await _context.SaveChangesAsync();

                // PASS Loot
                for (int row = 2; row <= endRow; row++)
                {
                    var monsterId = worksheet.Cells[row, 1].GetValue<int>();
                    var itemName = worksheet.Cells[row, 9].GetValue<string>();
                    var rarity = worksheet.Cells[row, 10].GetValue<string>();

                    if (string.IsNullOrWhiteSpace(itemName) || string.IsNullOrWhiteSpace(rarity))
                        continue;

                    if (!monstersById.ContainsKey(monsterId))
                        continue;

                    bool exists = existingLoot.Any(l =>
                        l.ItemName == itemName &&
                        l.Rarity == rarity &&
                        l.MonsterId == monsterId);

                    if (exists)
                        continue;

                    _context.Loots.Add(new Loot
                    {
                        ItemName = itemName,
                        Rarity = rarity,
                        MonsterId = monsterId
                    });

                    lootAdded++;
                }

                if (lootAdded > 0)
                    await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return Ok(new
            {
                MonstersAdded = monstersAdded,
                LootAdded = lootAdded
            });
        }
        [HttpPost("Users")]
        public async Task<ActionResult> PostUsers()
        {
            // Create User roles
            const string administrator = "administrator";
            const string registeredUser = "registeredUser";

            // If the roles don't exist, create them
            if (!await roleManager.RoleExistsAsync(administrator))
            {
                var x = await roleManager.CreateAsync(new IdentityRole(administrator));
            }

            if (!await roleManager.RoleExistsAsync(registeredUser))
            {
                var x = await roleManager.CreateAsync(new IdentityRole(registeredUser));
            }

            MonsterArchiveUser adminUser = new()
            {
                UserName = "admin",
                Email = "bryan.castro.789@gmail.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            await userManager.CreateAsync(adminUser, configuration["DefaultPasswords:admin"]!);

            MonsterArchiveUser regularUser = new()
            {
                UserName = "user",
                Email = "bryan.castro.0072@gmail.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            await userManager.CreateAsync(regularUser, configuration["DefaultPasswords:user"]!);
            
            await userManager.AddToRoleAsync(adminUser, administrator);
            await userManager.AddToRoleAsync(regularUser, registeredUser);
            return Ok();
        }
        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser(RegisterRequest registerRequest)
        {
            const string registeredUser = "registeredUser";                                             // Use default role for new users
            if (await userManager.FindByEmailAsync(registerRequest.Email) != null)                      // Check if user already exists
            {
                return BadRequest("User with this email already exists.");
            }

            var newUser = new MonsterArchiveUser                                                        // Create a new MonsterArchiveUser using the provided username, password, and email
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            await userManager.CreateAsync(newUser, registerRequest.Password);                           // Use provided username and password to create the user

            await userManager.AddToRoleAsync(newUser, registeredUser);                                  // Add the user to a default role (e.g., "registeredUser")

            // Optional: Send a confirmation email to the user


            return Ok(new RegisterResponse
            {
                Success = true,
                Message = "User registered successfully."
            });
        }
    }
}