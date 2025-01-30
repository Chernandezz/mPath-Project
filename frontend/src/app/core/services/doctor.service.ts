import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import {
  Doctor,
  CreateDoctorRequestDto,
  UpdateDoctorRequestDto,
} from '../models/doctor.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  private readonly route = 'Doctor';

  constructor(private httpService: HttpService) {}

  getAll(
    count: number,
    page: number,
    searchText: string
  ): Observable<{ data: Doctor[]; totalItems: number }> {
    
    return this.httpService.get<{ data: Doctor[]; totalItems: number }>(
      this.route,
      count,
      page,
      searchText
    );
  }

  getById(id: number): Observable<Doctor> {
    return this.httpService.get<Doctor>(`${this.route}/${id}`);
  }

  create(doctor: CreateDoctorRequestDto): Observable<Doctor> {
    return this.httpService.post<Doctor>(this.route, doctor);
  }

  update(id: number, doctor: UpdateDoctorRequestDto): Observable<void> {
    return this.httpService.put<void>(this.route, id, doctor);
  }

  delete(id: number): Observable<void> {
    
    return this.httpService.delete(this.route, id);
  }
}
