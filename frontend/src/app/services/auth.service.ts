import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5247/api/User';

  constructor(private http: HttpClient, private router: Router) {}

  login(email: string, password: string): Observable<any> {
    const url = `${this.apiUrl}/Login`;

    return this.http.post(url, { email, password }).pipe(
      tap(
        (response: any) => {
          if (response && response.accessToken) {
            localStorage.setItem('accessToken', response.accessToken);
          } else {
            console.warn('No accessToken found in response', response);
          }
        },
        (error) => {
          console.error('Error Response:', error);
        }
      )
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
