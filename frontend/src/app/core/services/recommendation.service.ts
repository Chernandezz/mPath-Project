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


  deactivateRecommendation(id: number): Observable<void> {
    return this.httpService.deactivate(this.route, id);
  }

  activateRecommendation(id: number): Observable<void> {
    return this.httpService.activate(this.route, id);
  }
}
