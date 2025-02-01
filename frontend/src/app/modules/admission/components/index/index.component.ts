import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { AdmissionService } from '../../../../core/services/admission.service';
import { Admission } from '../../../../core/models/admission.model';
import { FormComponent } from '../form/form.component';
import { DetailsDialogComponent } from '../details-dialog/details-dialog.component';
import { SharedModule } from '../../../global/shared.module';
import { AuthService } from '../../../../core/services/auth.service';
import { PatientService } from '../../../../core/services/patient.service';
import { DoctorService } from '../../../../core/services/doctor.service';

@Component({
  selector: 'app-index',
  imports: [SharedModule],
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
})
export class IndexComponent implements OnInit {
  displayedColumns = [
    'id',
    'admissionDate',
    'diagnosis',
    'observation',
    'doctorId',
    'patientId',
  ];
  dataSource = new MatTableDataSource<Admission>([]);
  totalItems = 0;
  pageCount = 10;
  pageNumber = 0;
  doctors: any[] = [];
  patients: any[] = [];
  searchText = '';
  role: string | null = null;
  userId = Number(localStorage.getItem('userId'));
  userRole = localStorage.getItem('role');
  paginationOptions: number[] = [5, 10, 25, 50];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private admissionService: AdmissionService,
    public dialog: MatDialog,
    private authService: AuthService,
    private httpDoctor: DoctorService,
    private httpPatient: PatientService
  ) {}

  ngOnInit(): void {
    this.loadAdmissions();
    this.loadDoctors();
    this.loadPatients();
    this.role = this.authService.getUserRole();
  }

  loadDoctors() {
    this.httpDoctor.getAll(100, 0, '').subscribe((response: any) => {
      this.doctors = response.data.filter((d: any) => d.active);
    });
  }
  getPatientName(patientId: number): string {
    const patient = this.patients.find((p) => p.id == patientId);
    return patient
      ? `[${patient.id}] ${patient.firstName} ${patient.lastName}`
      : 'Unknown';
  }

  getDoctorName(doctorId: number): string {
    const doctor = this.doctors.find((d) => d.id == doctorId);
    return doctor
      ? `[${doctor.id}] ${doctor.firstName} ${doctor.lastName}`
      : 'Unknown';
  }

  loadPatients() {
    this.httpPatient.getAll(100, 0, '').subscribe((response: any) => {
      this.patients = response.data;
    });
  }
  loadAdmissions() {
    if (this.userRole === 'Patient') {
      this.admissionService
        .getAllByUserId(
          this.pageCount,
          this.pageNumber,
          this.searchText,
          this.userId
        )
        .subscribe((response) => {
          this.dataSource.data = response.data;
          this.totalItems = response.totalItems;
        });
    } else {
      this.admissionService
        .getAll(this.pageCount, this.pageNumber, this.searchText)
        .subscribe((response) => {
          this.dataSource.data = response.data;
          this.totalItems = response.totalItems;
        });
    }
  }

  handlePageEvent(event: PageEvent) {
    this.pageCount = event.pageSize;
    this.pageNumber = event.pageIndex;
    this.loadAdmissions();
  }

  createAdmission() {
    const dialogRef = this.dialog.open(FormComponent, {
      width: '700px',
      data: { type: 'Create' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== false) this.loadAdmissions();
    });
  }

  openDetails(admission: Admission) {
    this.dialog.open(DetailsDialogComponent, {
      data: admission,
      width: '500px',
    });
  }
}
