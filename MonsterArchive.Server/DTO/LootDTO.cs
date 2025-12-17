namespace MonsterArchive.Server.DTO
{
    public class LootDTO
    {
        public required string ItemName { get; set; }
        public required string Rarity { get; set; }
        public required int MonsterId { get; set; }
    }
}
