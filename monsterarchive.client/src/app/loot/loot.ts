import { HttpClient } from '@angular/common/http';
import { Component} from '@angular/core';
import { LootData } from './loot-data';
import { RouterLink } from '@angular/router';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { AsyncPipe, NgIf } from '@angular/common';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-loot',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './loot.html',
  styleUrls: ['./loot.scss']
})

export class Loot{
  loot: any;
  loots$: Observable<LootData[]>;
  constructor(http: HttpClient) {
    this.loots$ = http.get<LootData[]>(environment.apiUrl + 'api/Loots');
  }
}
