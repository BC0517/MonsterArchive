using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MonsterArchive.Server.Data.Models
{
    public class MonsterEntityTypeConfiguration : IEntityTypeConfiguration<Monster>
    {
        public void Configure(EntityTypeBuilder<Monster> builder)
        {
            builder.ToTable("Monsters");

            builder.HasKey(x => x.MonsterId);                       // PK = MonsterId
            builder.Property(x => x.MonsterId).IsRequired();        // MonsterId is required

            builder.Property(x => x.Name).IsRequired()              // Name is required
                   .HasMaxLength(100)                               // Max length 100
                   .IsUnicode(false);                               // Non-Unicode

            builder.Property(x => x.Species).IsRequired()           // Species is required
                   .HasMaxLength(100)                               // Max length 100
                   .IsUnicode(false);                               // Non-Unicode

            builder.Property(x => x.Element).IsRequired()           // Element is required
                   .HasMaxLength(100)                               // Max length 100
                   .IsUnicode(false);                               // Non-Unicode

            builder.Property(x => x.Weakness).IsRequired()          // Weakness is required
                   .HasMaxLength(100)                               // Max length 100
                   .IsUnicode(false);                               // Non-Unicode

            builder.Property(x => x.Rank).IsRequired()              // Rank is required
                   .HasMaxLength(50)                                // Max length 50
                   .IsUnicode(false);                               // Non-Unicode

            builder.Property(x => x.AggressionLevel).IsRequired()   // AggressionLevel is required
                   .HasMaxLength(50)                                // Max length 50
                   .IsUnicode(false);                               // Non-Unicode
        }
    }
}