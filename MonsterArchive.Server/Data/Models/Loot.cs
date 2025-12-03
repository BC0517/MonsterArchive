using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MonsterArchive.Server.Data.Models
{
    [Table("Loots")]
    [Index(nameof(ItemName))]                               // Index on ItemName for faster searches
    [Index(nameof(Rarity))]                                 // Index on Rarity for filtering
    [Index(nameof(MonsterId))]                              // Index on MonsterId for join performance
    public class Loot
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }                         // The unique id and primary key for this Loot

        [Column("itemName")]
        public required string ItemName { get; set; }       // Name of the loot item

        [Column("rarity")]
        public required string Rarity { get; set; }         // Rarity of the loot item

        [Column("monsterId")]
        public int MonsterId { get; set; }                  // Foreign key to the Monster entity

        [ForeignKey(nameof(MonsterId))]
        public Monster? Monster { get; set; }               // Navigation property to the related Monster
    }
}
