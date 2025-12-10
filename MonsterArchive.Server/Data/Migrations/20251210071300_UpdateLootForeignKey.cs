using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonsterArchive.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLootForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loot_Monsters_monsterId",
                table: "Loot");

            migrationBuilder.RenameColumn(
                name: "monsterId",
                table: "Loot",
                newName: "MonsterId");

            migrationBuilder.RenameIndex(
                name: "IX_Loot_monsterId",
                table: "Loot",
                newName: "IX_Loot_MonsterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loot_Monsters_MonsterId",
                table: "Loot",
                column: "MonsterId",
                principalTable: "Monsters",
                principalColumn: "monsterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loot_Monsters_MonsterId",
                table: "Loot");

            migrationBuilder.RenameColumn(
                name: "MonsterId",
                table: "Loot",
                newName: "monsterId");

            migrationBuilder.RenameIndex(
                name: "IX_Loot_MonsterId",
                table: "Loot",
                newName: "IX_Loot_monsterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loot_Monsters_monsterId",
                table: "Loot",
                column: "monsterId",
                principalTable: "Monsters",
                principalColumn: "monsterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
