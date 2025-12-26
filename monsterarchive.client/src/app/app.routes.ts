import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './home/home';
import { Loot } from './loot/loot';
import { Monster } from './monster/monster';
import { Login } from './auth/login';
import { LootDetailsComponent } from './loot-details/loot-details';
import { Register } from './auth/register/register';

export const routes: Routes = [
  { path: '', component: Home, pathMatch: 'full' },
  { path: 'loot', component: Loot},
  { path: 'monster', component: Monster },
  { path: 'login', component: Login },
  { path: 'lootDetails/:id', component: LootDetailsComponent },
  { path: 'register', component: Register }
];