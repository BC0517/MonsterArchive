using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Metrics;
namespace MonsterArchive.Server.Data.Models
{
    public class MonsterEntityTypeConfiguration : IEntityTypeConfiguration<Monster>
    {
        public void Configure(EntityTypeBuilder<Monster> builder)
        {
            builder.ToTable("Monsters");                                // Map to "Monsters" table
            builder.HasKey(x => x.Id);                                  // Set primary key
            builder.Property(x => x.Id).IsRequired();                   // Id is required

            builder.Property(x => x.Name).IsRequired()                  // Name is required
                     .HasMaxLength(100)                                 // Max length of 100
                     .IsUnicode(false);                                 // Non-Unicode string

            builder.Property(x => x.Species).IsRequired()               // Species is required
                .HasMaxLength(100)                                      // Max length of 100
                .IsUnicode(false);                                      // Non-Unicode string

            builder.Property(x => x.Element).IsRequired()               // Element is required
                .HasMaxLength(100)                                      // Max length of 100
                .IsUnicode(false);                                      // Non-Unicode string

            builder.Property(x => x.Weakness).IsRequired()              // Weakness is required
                .HasMaxLength(100)                                      // Max length of 100
                .IsUnicode(false);                                      // Non-Unicode string

            builder.Property(x => x.Rank).IsRequired()                 // Rank is required
                .HasMaxLength(50)                                       // Max length of 50
                .IsUnicode(false);                                      // Non-Unicode string

            builder.Property(x => x.AggressionLevel).IsRequired()      // AggressionLevel is required
                .HasMaxLength(50)                                       // Max length of 50
                .IsUnicode(false);                                      // Non-Unicode string
        }
    }
}
