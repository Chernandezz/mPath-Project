import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { AdmissionService } from '../../../../core/services/admission.service';
import { Admission } from '../../../../core/models/admission.model';
import { FormComponent } from '../form/form.component';
import { DetailsDialogComponent } from '../details-dialog/details-dialog.component';
import { SharedModule } from '../../../global/shared.module';

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
  searchText = '';
  userId = Number(localStorage.getItem('userId'));
  userRole = localStorage.getItem('role');
  paginationOptions: number[] = [5, 10, 25, 50];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private admissionService: AdmissionService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadAdmissions();
  }

  loadAdmissions() {
    if(this.userRole === 'Admin') {
      this.admissionService
        .getAll(this.pageCount, this.pageNumber, this.searchText)
        .subscribe((response) => {
          this.dataSource.data = response.data;
          this.totalItems = response.totalItems;
        });
    }else{
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
