import { LootList } from "../loot-details/loot-list";

export interface MonsterData {
  monsterId: number;
  name: string;
  species: string;
  element: string;
  weakness: string;
  rank: string;
  aggressionLevel: string;
  loots: LootList[];
}