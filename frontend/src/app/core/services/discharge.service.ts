import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { CreateDischargeRequestDto, Discharge } from '../models/discharge.model';

@Injectable({
  providedIn: 'root',
})
export class DischargeService {
  private readonly route = 'Discharge';

  constructor(private httpService: HttpService) {}

  getAll(
    count: number,
    page: number,
    searchText: string
  ): Observable<{ data: Discharge[]; totalItems: number }> {
    return this.httpService.get<{ data: Discharge[]; totalItems: number }>(
      this.route,
      count,
      page,
      searchText
    );
  }

  getById(id: number): Observable<Discharge> {
    return this.httpService.get<Discharge>(`${this.route}/${id}`);
  }

  create(discharge: CreateDischargeRequestDto): Observable<Discharge> {
    return this.httpService.post<Discharge>(this.route, discharge);
  }
}
