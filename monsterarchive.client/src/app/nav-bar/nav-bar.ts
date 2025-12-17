import { Component } from '@angular/core';
import { RouterLink } from "@angular/router";
import { AngularMaterialModule } from '../angular-material';
import { AuthService } from '../auth/auth-service';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink, AngularMaterialModule],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.scss'
})
export class NavBar {
  constructor(public authService: AuthService) {}
}