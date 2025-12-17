namespace MonsterArchive.Server.DTO
{
    public class MonsterDTO
    {
        public required int MonsterId { get; set; }
        public required string Name { get; set; }
        public required string Species { get; set; }
        public required string Element { get; set; }
        public required string Weakness { get; set; }
        public required string Rank { get; set; }
        public required string AggressionLevel { get; set; }
    }
}
