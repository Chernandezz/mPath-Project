import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { LoginRequestDto, LoginResponseDto } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5247/api/User';

  constructor(private http: HttpClient, private router: Router) {}

  login(request: LoginRequestDto): Observable<LoginResponseDto> {
    return this.http
      .post<LoginResponseDto>(`${this.apiUrl}/Login`, request)
      .pipe(
        tap((response: LoginResponseDto) => {
          if (response && response.accessToken) {
            localStorage.setItem('accessToken', response.accessToken);
          }
        })
      );
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('accessToken');
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }
}
