import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private apiBase = 'http://localhost:5247/api';

  constructor(private httpClient: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const accessToken = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${accessToken}`,
      'Content-Type': 'application/json',
    });
  }

  get<T>(
    route: string,
    count: number = 10,
    page: number = 0,
    searchText: string = ''
  ): Observable<T> {
    const params = new HttpParams()
      .set('count', count.toString())
      .set('page', page.toString())
      .set('searchText', searchText);

    return this.httpClient.get<T>(`${this.apiBase}/${route}`, {
      headers: this.getAuthHeaders(),
      params,
    });
  }

  post<T>(route: string, body: any): Observable<T> {
    return this.httpClient.post<T>(`${this.apiBase}/${route}`, body, {
      headers: this.getAuthHeaders(),
    });
  }

  put<T>(route: string, id: number, body: any): Observable<T> {
    return this.httpClient.put<T>(`${this.apiBase}/${route}/${id}`, body, {
      headers: this.getAuthHeaders(),
    });
  }

  delete(route: string, id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiBase}/${route}/${id}`);
  }
}
