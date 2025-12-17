using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MonsterArchive.Server.Data.Models;
using System.Diagnostics.Metrics;
namespace MonsterArchive.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<MonsterArchiveUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        public DbSet<Loot> Loots => Set<Loot>();
        public DbSet<Monster> Monsters => Set<Monster>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LootEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MonsterEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

}
