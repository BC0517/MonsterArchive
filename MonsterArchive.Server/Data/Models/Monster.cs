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
        [Column("id")]
        public int Id { get; set; }                                         // The unique id and primary key for this City

        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public required string Name { get; set; }                           // Monster name (in UTF8 format)

        [Column("species")]
        public required string Species { get; set; }                        // Monster species

        [Column("element")]
        public required string Element { get; set; }                        // Monster elemental type

        [Column("weakness")]
        public required string Weakness { get; set; }                       // Monster weakness type

        [Column("rank")]
        public required string Rank { get; set; }                           // Monster rank (e.g., Low, High, M)

        [Column("aggressionLevel")]
        public required string AggressionLevel { get; set; }                // Monster aggression level

        public ICollection<Loot> Loots { get; set; } = new List<Loot>();    // Navigation property for related Loot items
    }
}
