import { AsyncPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { MonsterData } from './monster-data';

@Component({
  selector: 'app-monster',
  imports: [RouterLink,AsyncPipe],
  templateUrl: './monster.html',
  styleUrl: './monster.scss',
})
export class Monster {
  monster: any;
  monsters$: Observable<MonsterData[]>;
  constructor(http: HttpClient) {
    this.monsters$ = http.get<MonsterData[]>(environment.apiUrl + 'api/Monsters');
  }

}
