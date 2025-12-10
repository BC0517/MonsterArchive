using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MonsterArchive.Server.Data.Models
{
    [Table("Loot")]
    [Index(nameof(LootId))]
    [Index(nameof(ItemName))]                               // Index on ItemName for faster searches
    [Index(nameof(Rarity))]                                 // Index on Rarity for filtering
    [Index(nameof(MonsterId))]                              // Index on MonsterId for join performance
    public class Loot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // <-- Auto-increment
        [Column("lootId")]
        public int LootId { get; set; }                         // Primary key

        [Column("itemName")]
        public required string ItemName { get; set; }           // Name of the loot item

        [Column("rarity")]
        public required string Rarity { get; set; }             // Rarity of the loot item

        [ForeignKey(nameof(Monster))]
        public int MonsterId { get; set; }                      // Foreign key to Monster

        public Monster? Monster { get; set; }                   // Navigation property to Monster
    }
}
