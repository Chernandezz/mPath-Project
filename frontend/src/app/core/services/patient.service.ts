import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import {
  Patient,
  CreatePatientRequestDto,
  UpdatePatientRequestDto,
} from '../models/patient.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  private readonly route = 'Patient';

  constructor(private httpService: HttpService) {}

  getAll(
    count: number,
    page: number,
    searchText: string
  ): Observable<{ data: Patient[]; totalItems: number }> {
    return this.httpService.get<{ data: Patient[]; totalItems: number }>(
      this.route,
      count,
      page,
      searchText
    );
  }

  getById(id: number): Observable<Patient> {
    return this.httpService.get<Patient>(`${this.route}/${id}`);
  }

  create(patient: CreatePatientRequestDto): Observable<Patient> {
    return this.httpService.post<Patient>(this.route, patient);
  }

  update(id: number, patient: UpdatePatientRequestDto): Observable<void> {
    return this.httpService.put<void>(this.route, id, patient);
  }


}
