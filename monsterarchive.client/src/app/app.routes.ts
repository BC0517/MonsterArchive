import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './home/home';
import { Loot } from './loot/loot';
import { Monster } from './monster/monster';

export const routes: Routes = [
  { path: '', component: Home, pathMatch: 'full' },
  { path: 'loot', component: Loot},
  { path: 'monster', component: Monster }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
