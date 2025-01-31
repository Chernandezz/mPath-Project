import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import {
  Admission,
  CreateAdmissionRequestDto,
} from '../models/admission.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AdmissionService {
  private readonly route = 'Admission';

  constructor(private httpService: HttpService, private httpClient: HttpClient) {}

  getAll(
    count: number,
    page: number,
    searchText: string
  ): Observable<{ data: Admission[]; totalItems: number }> {
    return this.httpService.get<{ data: Admission[]; totalItems: number }>(
      this.route,
      count,
      page,
      searchText
    );
  }

  getById(id: number): Observable<Admission> {
    return this.httpService.get<Admission>(`${this.route}/${id}`);
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
