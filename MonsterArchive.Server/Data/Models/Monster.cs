using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MonsterArchive.Server.Data.Models
{
    [Table("Monsters")]
    [Index(nameof(Name))]                                                   // Index on Name for faster searches
    [Index(nameof(Species))]                                                // Index on Species for filtering
    [Index(nameof(Element))]                                                // Index on Element for filtering
    [Index(nameof(Weakness))]                                               // Index on Weakness for filtering
    [Index(nameof(Rank))]                                                   // Index on Rank for filtering
    [Index(nameof(AggressionLevel))]                                        // Index on AggressionLevel for filtering
    public class Monster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]   // <-- No auto-increment, we use Excel MonsterId
        [Column("monsterId")]
        public int MonsterId { get; set; }                      // Primary key

        [Column("name")]                                    
        [StringLength(100)]
        [Unicode(false)]
        public required string Name { get; set; }               // Name of the monster

        [Column("species")]                             
        public required string Species { get; set; }            // Species of the monster

        [Column("element")]
        public required string Element { get; set; }            // Element of the monster

        [Column("weakness")]
        public required string Weakness { get; set; }           // Weakness of the monster

        [Column("rank")]
        public required string Rank { get; set; }               // Rank of the monster

        [Column("aggressionLevel")]
        public required string AggressionLevel { get; set; }    // Aggression level of the monster

        public ICollection<Loot> Loots { get; set; } = new List<Loot>();
    }

}
