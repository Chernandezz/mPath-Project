import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private apiBase = environment.apiBase;

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
    this.logAction('CREATE', route, undefined, 'Created a new entity');
    return this.httpClient.post<T>(`${this.apiBase}/${route}`, body, {
      headers: this.getAuthHeaders(),
    });
  }

  put<T>(route: string, id: number, body: any): Observable<T> {
     this.logAction('UPDATE', route, id, 'Updated entity');
    return this.httpClient.put<T>(`${this.apiBase}/${route}/${id}`, body, {
      headers: this.getAuthHeaders(),
    });
  }

  deactivate<T>(route: string, id: number): Observable<T> {
    this.logAction('DEACTIVATE', route, id, 'Doctor deactivated');
    return this.httpClient.put<T>(
      `${this.apiBase}/${route}/${id}/deactivate`,
      null,
      {
        headers: this.getAuthHeaders(),
      }
    );
  }

  activate<T>(route: string, id: number): Observable<T> {
    this.logAction('ACTIVATE', route, id, 'Doctor activated');
    return this.httpClient.put<T>(
      `${this.apiBase}/${route}/${id}/activate`,
      null,
      {
        headers: this.getAuthHeaders(),
      }
    );
  }

  getAllRecommendationsById<T>(
    count: number = 10,
    page: number = 0,
    searchText: string = '',
    userId: number
  ): Observable<T> {
    const params = new HttpParams()
      .set('count', count.toString())
      .set('page', page.toString())
      .set('searchText', searchText)
      .set('userId', userId.toString());

    return this.httpClient.get<T>(`${this.apiBase}/Discharge/recommendations`, {
      headers: this.getAuthHeaders(),
      params,
    });
  }

  deactivateRecommendation<T>(route: string, id: number): Observable<T> {
    return this.httpClient.put<T>(
      `${this.apiBase}/${route}/${id}/deactivate`,
      null,
      {
        headers: this.getAuthHeaders(),
      }
    );
  }

  public logAction(
    action: string,
    entity: string,
    entityId?: number,
    details?: string
  ) {
    const userId = localStorage.getItem('userId');
    if (!userId) return;

    const log = {
      UserId: userId,
      Action: action,
      Entity: entity,
      EntityId: entityId,
      Details: details,
    };

    this.httpClient
      .post(`${this.apiBase}/AuditLog/create-log`, log, {
        headers: this.getAuthHeaders(),
      })
      .subscribe();
  }

  activateRecommendation<T>(route: string, id: number): Observable<T> {
    return this.httpClient.put<T>(
      `${this.apiBase}/${route}/${id}/activate`,
      null,
      {
        headers: this.getAuthHeaders(),
      }
    );
  }
}
