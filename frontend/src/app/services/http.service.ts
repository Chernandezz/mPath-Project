import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(private httpClient: HttpClient) {}

  private apiBase = 'http://localhost:7177/api';

  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }

  GetAll(count: number, page: number, searchText: string, route: string) {
    let params = new HttpParams()
      .set('count', count.toString())
      .set('page', page.toString())
      .set('searchText', searchText);

    return this.httpClient.get(`${this.apiBase}/${route}`, {
      params,
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
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  login(email: string, password: string) {
    console.log('test');

    return this.httpClient.post(`${this.apiBase}/auth/login`, {
      email,
      password,
    });
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

    return this.httpClient.post('http://localhost:54756/api/Admission', body, {
      headers: headers,
    });
  }
}
