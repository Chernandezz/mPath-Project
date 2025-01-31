import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { LoginRequestDto, LoginResponseDto } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:8080/api/User';

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

  getUserRole(): string | null {
    const token = this.getToken();

    if (!token) return null;

    const payload = this.decodeToken(token);

    return payload?.role || null;
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  isTokenValid(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const payload = this.decodeToken(token);
    if (!payload || !payload.exp) return false;

    const currentTime = Math.floor(Date.now() / 1000);
    return payload.exp > currentTime;
  }

  private decodeToken(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      return JSON.parse(atob(base64));
    } catch (error) {
      return null;
    }
  }
}
