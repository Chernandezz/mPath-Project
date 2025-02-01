import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Recommendation } from '../models/recommendation.model';

@Injectable({
  providedIn: 'root',
})
export class RecommendationService {
  private readonly route = 'Discharge'; // Usar la misma estructura que Admission

  constructor(
    private httpService: HttpService,
    private httpClient: HttpClient
  ) {}

  getAll(
    count: number,
    page: number,
    searchText: string
  ): Observable<{ data: Recommendation[]; totalItems: number }> {
    return this.httpService.get<{ data: Recommendation[]; totalItems: number }>(
      this.route,
      count,
      page,
      searchText
    );
  }

  getAllByUserId(
    count: number,
    page: number,
    searchText: string,
    userId: number
  ): Observable<{ data: Recommendation[]; totalItems: number }> {
    return this.httpService.get<{ data: Recommendation[]; totalItems: number }>(
      `${this.route}/recommendations/?userId=${userId}`,
      count,
      page,
      searchText
    );
  }

  getById(id: number): Observable<Recommendation> {
    return this.httpService.get<Recommendation>(`${this.route}/${id}`);
  }

  deactivateRecommendation(id: number): Observable<void> {
    return this.httpService.deactivate(this.route, id);
  }

  activateRecommendation(id: number): Observable<void> {
    return this.httpService.activate(this.route, id);
  }
}
