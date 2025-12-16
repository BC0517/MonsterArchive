import { HttpClient } from '@angular/common/http';
import { Component} from '@angular/core';
import { LootData } from './loot-data';
import { RouterLink } from '@angular/router';
import { environment } from '../../environments/environment.development';

@Component({
  selector: 'app-loot',
  imports: [RouterLink],
  templateUrl: './loot.component.html',
  styleUrl: './loot.component.scss'
})

export class LootComponent {

  countries: LootData[] = [];
  constructor(http: HttpClient) {
    http.get<LootData[]>(environment.apiUrl + "api/Loots").subscribe(result => {
      this.countries = result;
    });
  }
}
