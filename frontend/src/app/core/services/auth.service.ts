import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiBase}/User`;
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(
    this.isLoggedIn()
  );

  constructor(private httpClient: HttpClient, private router: Router) {}

  login(email: string, password: string): Observable<any> {
    const url = `${this.apiUrl}/Login`;

    return this.httpClient.post(url, { email, password }).pipe(
      tap((response: any) => {
        if (response?.accessToken) {
          localStorage.setItem('accessToken', response.accessToken);
          this.isAuthenticatedSubject.next(true);
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

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    this.isAuthenticatedSubject.next(false);
    this.router.navigate(['/login']);
  }

  isAuthenticated(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
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
    } catch {
      return null;
    }
  }
}
