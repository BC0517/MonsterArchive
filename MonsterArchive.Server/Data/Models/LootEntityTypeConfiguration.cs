using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MonsterArchive.Server.Data.Models
{
    public class LootEntityTypeConfiguration : IEntityTypeConfiguration<Loot>
    {
        public void Configure(EntityTypeBuilder<Loot> builder)
        {
            builder.ToTable("Loot");                                        // Map to "Loot" table

            builder.HasKey(x => x.LootId);                                  // Primary key
            builder.Property(x => x.LootId).IsRequired();                   // Id is required

            builder.Property(x => x.ItemName).IsRequired()                  // ItemName is required
                .HasMaxLength(100)                                          // Max length 100
                .IsUnicode(false);                                          // Non-Unicode string


            builder.Property(x => x.Rarity).IsRequired()                    // Rarity is required
                .HasMaxLength(50)                                           // Max length 50
                .IsUnicode(false);                                          // Non-Unicode string

            builder.HasOne(x => x.Monster)                                  // Relationship with Monster
                .WithMany(x => x.Loots)                                     // A Monster has many Loots
                .HasForeignKey(x => x.MonsterId);                           // Foreign key
        }
    }
}
