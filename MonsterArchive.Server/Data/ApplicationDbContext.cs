using Microsoft.EntityFrameworkCore;
using MonsterArchive.Server.Data.Models;
using System.Diagnostics.Metrics;
namespace MonsterArchive.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }
        public ApplicationDbContext(DbContextOptions options)
         : base(options)
        {
        }
        public DbSet<Loot> Loots => Set<Loot>();
        public DbSet<Monster> Monsters => Set<Monster>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").AddJsonFile("appsettings.Development.json", optional: true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LootEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MonsterEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        // Alternatively, you can use the following method to automatically apply all configurations from the assembly
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    // add the EntityTypeConfiguration classes
        //    modelBuilder.ApplyConfigurationsFromAssembly(
        //        typeof(ApplicationDbContext).Assembly
        //        );
        //}
    }

}
