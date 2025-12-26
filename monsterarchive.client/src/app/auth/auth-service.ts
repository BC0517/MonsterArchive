import { Injectable } from '@angular/core';
import { LoginRequest } from './login-request';
import { LoginResponse } from './login-response';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { RegisterResponse } from './register/register-response';
import { RegisterRequest } from './register/register-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private token = 'auth_token';
  private _authStatus = new BehaviorSubject<boolean>(false);
  public authStatus = this._authStatus.asObservable();

  constructor(private http: HttpClient) {}

  init(){
    if(this.isLoggedIn()){
      this.setAuthStatus(true);
    }
  }

  setAuthStatus(isLoggedIn: boolean){
    this._authStatus.next(isLoggedIn);
  }

  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(environment.apiUrl + "api/admin/", loginRequest)
      .pipe(tap(response => {
        if (response.success){
          localStorage.setItem(this.token, response.token);
          this.setAuthStatus(true);
        }
      }))
  }

  register(registerRequest: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(environment.apiUrl + "api/Seed/RegisterUser", registerRequest);
  }

  getToken(): string | null {
    return localStorage.getItem(this.token);
  }

  logout(){
    localStorage.removeItem(this.token);
    this.setAuthStatus(false);
  }
  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }
}