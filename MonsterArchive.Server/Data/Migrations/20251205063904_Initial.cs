using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonsterArchive.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    species = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    element = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    weakness = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    rank = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    aggressionLevel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Loot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    rarity = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    monsterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loot", x => x.id);
                    table.ForeignKey(
                        name: "FK_Loot_Monsters_monsterId",
                        column: x => x.monsterId,
                        principalTable: "Monsters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loot_itemName",
                table: "Loot",
                column: "itemName");

            migrationBuilder.CreateIndex(
                name: "IX_Loot_monsterId",
                table: "Loot",
                column: "monsterId");

            migrationBuilder.CreateIndex(
                name: "IX_Loot_rarity",
                table: "Loot",
                column: "rarity");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_aggressionLevel",
                table: "Monsters",
                column: "aggressionLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_element",
                table: "Monsters",
                column: "element");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_name",
                table: "Monsters",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_rank",
                table: "Monsters",
                column: "rank");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_species",
                table: "Monsters",
                column: "species");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_weakness",
                table: "Monsters",
                column: "weakness");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loot");

            migrationBuilder.DropTable(
                name: "Monsters");
        }
    }
}
