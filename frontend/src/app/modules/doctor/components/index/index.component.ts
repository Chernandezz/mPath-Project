import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DoctorService } from '../../../../core/services/doctor.service';
import { Doctor } from '../../../../core/models/doctor.model';
import { FormComponent } from '../form/form.component';
import { DetailsDialogComponent } from '../details-dialog/details-dialog.component';
import { SharedModule } from '../../../global/shared.module';
import { catchError, tap, throwError } from 'rxjs';

@Component({
  selector: 'app-index',
  imports: [SharedModule],
  templateUrl: './index.component.html',
})
export class IndexComponent implements OnInit {
  displayedColumns = ['id', 'firstName', 'lastName', 'actions'];
  dataSource = new MatTableDataSource<Doctor>([]);
  totalItems = 0;
  pageCount = 10;
  pageNumber = 0;
  searchText = '';
  paginationOptions: number[] = [1, 5, 10, 25, 100];

  constructor(private doctorService: DoctorService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors() {
    this.doctorService
      .getAll(this.pageCount, this.pageNumber, this.searchText)
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.data;
          this.totalItems = response.totalItems;
        },
        error: (error) =>
          console.error('Subscription error in loadDoctors:', error),
      });
  }

  delete(doctorId: number) {
    if (confirm(`Are you sure you want to remove doctor (ID: ${doctorId})?`)) {
      this.doctorService.delete(doctorId).subscribe(() => {
        this.loadDoctors();
      });
    }
  }

  createDoctor() {
    const dialogRef = this.dialog.open(FormComponent, {
      width: '700px',
      data: { type: 'Create' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== false) this.loadDoctors();
    });
  }

  openDetails(doctor: Doctor) {
    this.dialog.open(DetailsDialogComponent, { data: doctor, width: '500px' });
  }

  handlePageEvent(event: any) {
    this.pageCount = event.pageSize;
    this.pageNumber = event.pageIndex;

    this.loadDoctors();
  }
}
