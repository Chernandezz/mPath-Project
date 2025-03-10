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

  constructor(
    private httpService: HttpService,
    private httpClient: HttpClient
  ) {}

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

  getAllByUserId(
    count: number,
    page: number,
    searchText: string,
    userId: number
  ): Observable<{ data: Admission[]; totalItems: number }> {
    return this.httpService.get<{ data: Admission[]; totalItems: number }>(
      `${this.route}/admissionbyid/?userId=${userId}`,
      count,
      page,
      searchText
    );
  }


  create(admission: CreateAdmissionRequestDto): Observable<Admission> {
    return this.httpService.post<Admission>(this.route, admission);
  }
}
