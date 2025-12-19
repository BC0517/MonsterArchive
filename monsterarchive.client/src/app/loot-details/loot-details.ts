import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { environment } from '../../environments/environment';
import { CommonModule } from '@angular/common';
import { MonsterData } from '../monster/monster-data';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-loot-details',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './loot-details.html',
  styleUrls: ['./loot-details.scss']
})
export class LootDetailsComponent {

  lootList$ = this.route.paramMap.pipe(
    map(params => params.get('id')),
    switchMap(id =>
      this.http.get<MonsterData>(
        `${environment.apiUrl}api/Monsters/${id}`
      )
    ),
    map(monster => monster.loots)
  );

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
  ) {}
}
