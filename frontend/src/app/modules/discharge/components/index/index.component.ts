import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { DischargeService } from '../../../../core/services/discharge.service';
import { Discharge } from '../../../../core/models/discharge.model';
import { SharedModule } from '../../../global/shared.module';
import { FormComponent } from '../form/form.component';
import { DetailsDialogComponent } from '../details-dialog/details-dialog.component';

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
    'treatment',
    'dischargeDate',
    'amount',
    'isPaid',
    'admissionId',
  ];
  dataSource = new MatTableDataSource<Discharge>([]);
  totalItems = 0;
  pageCount = 10;
  pageNumber = 0;
  searchText = '';
  paginationOptions: number[] = [5, 10, 25, 50];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private dischargeService: DischargeService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadDischarges();
  }

  loadDischarges() {
    this.dischargeService
      .getAll(this.pageCount, this.pageNumber, this.searchText)
      .subscribe((response) => {
        this.dataSource.data = response.data;
        this.totalItems = response.totalItems;
      });
  }

  handlePageEvent(event: PageEvent) {
    this.pageCount = event.pageSize;
    this.pageNumber = event.pageIndex;
    this.loadDischarges();
  }

  createDischarge() {
    const dialogRef = this.dialog.open(FormComponent, {
      width: '700px',
      data: { type: 'Create' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== false) this.loadDischarges();
    });
  }

  openDetails(discharge: Discharge) {
    this.dialog.open(DetailsDialogComponent, {
      data: discharge,
      width: '500px',
    });
  }
}
