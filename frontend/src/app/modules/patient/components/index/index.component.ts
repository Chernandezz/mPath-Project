import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { PatientService } from '../../../../core/services/patient.service';
import { Patient } from '../../../../core/models/patient.model';
import { FormComponent } from '../form/form.component';
import { DetailsDialogComponent } from '../details-dialog/details-dialog.component';
import { SharedModule } from '../../../global/shared.module';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
})
export class IndexComponent implements OnInit {
  displayedColumns = [
    'id',
    'firstName',
    'lastName',
    'address',
    'phoneNumber',
    'observations'
  ];
  dataSource = new MatTableDataSource<Patient>([]);
  totalItems = 0;
  pageCount = 10;
  pageNumber = 0;
  searchText = '';
  paginationOptions: number[] = [5, 10, 25, 50];

  constructor(
    private patientService: PatientService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients() {
    this.patientService
      .getAll(this.pageCount, this.pageNumber, this.searchText)
      .subscribe((response) => {
        this.dataSource.data = response.data;
        this.totalItems = response.totalItems;
      });
  }

  handlePageEvent(event: any) {
    this.pageCount = event.pageSize;
    this.pageNumber = event.pageIndex;
    this.loadPatients();
  }

  createPatient() {
    const dialogRef = this.dialog.open(FormComponent, {
      width: '700px',
      data: { type: 'Create' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== false) this.loadPatients();
    });
  }

  openDetails(patient: Patient) {
    this.dialog.open(DetailsDialogComponent, { data: patient, width: '500px' });
  }


}
