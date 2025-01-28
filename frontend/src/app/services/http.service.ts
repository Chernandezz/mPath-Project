import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(private httpClient: HttpClient) {}

  private apiBase = 'http://localhost:8080/api';

  private getAuthHeaders() {
    const accessToken = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${accessToken}`,
    });
  }

  GetAll(count: number, page: number, searchText: string, route: string) {
    const params = new HttpParams()
      .set('count', count.toString())
      .set('page', page.toString())
      .set('searchText', searchText);

    const url = `${this.apiBase}/${route}?${params.toString()}`;
    console.log(`🔹 GET request to: ${url}`);

    return this.httpClient.get(url, {
      headers: this.getAuthHeaders(),
    });
  }

  Delete(ids: number[]) {
    return this.httpClient.delete(`${this.apiBase}/Doctor`, {
      headers: this.getAuthHeaders(),
      body: ids,
    });
  }

  logout(): void {
    localStorage.removeItem('accessToken');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('accessToken');
  }

  getaccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  login(email: string, password: string): Observable<any> {
    const url = `${this.apiBase}/User/Login`;
    console.log(`POST request to: ${url}`);

    return this.httpClient.post(url, { email, password }).pipe(
      tap(
        (response: any) => {
          console.log(`Response:`, response);
          if (response && response.accessToken) {
            localStorage.setItem('token', response.accessToken);
          } else {
            console.warn(`No accessToken found in response`);
          }
        },
        (error) => {
          console.error(`Error Response:`, error);
        }
      )
    );
  }

  CreateDoctor(
    id: number,
    firstName: string,
    lastName: string,
    active: boolean,
    email: string
  ) {
    const body = { id, firstName, lastName, active, email };
    return this.httpClient.post(`${this.apiBase}/Doctor`, body, {
      headers: this.getAuthHeaders(),
    });
  }

  CreateAdmission(
    patientId: number,
    doctorId: number,
    admissionDate: string,
    diagnosis: string,
    observation: string
  ) {
    const body = {
      patientId: patientId,
      doctorId: doctorId,
      admissionDate: admissionDate,
      diagnosis: diagnosis,
      observation: observation,
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.httpClient.post('http://localhost:8080/api/Admission', body, {
      headers: headers,
    });
  }
}
