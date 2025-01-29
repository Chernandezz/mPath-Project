import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../../global/shared.module';
import { HttpService } from '../../../../services/http.service';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { FormComponent } from '../form/form.component';
import { DetailsDialogComponent } from '../details-dialog/details-dialog.component';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss',
})
export class IndexComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'email',
    'actions',
  ];
  dataSource = new MatTableDataSource<any>([]);

  totalItems = 0;
  pageCount = 10;
  pageNumber = 0;
  paginationOptions: number[] = [1, 5, 10, 25, 100];

  searchText = '';

  openDetails(row: any) {
    this.dialog.open(DetailsDialogComponent, {
      data: row,
      width: '500px',
    });
  }
  constructor(
    private httpService: HttpService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.GetAll();
  }

  GetAll() {
    this.httpService
      .GetAll(this.pageCount, this.pageNumber, this.searchText, 'Doctor')
      .subscribe((response: any) => {
        this.dataSource.data = response.data;
        this.totalItems = response.totalItems;
      });
  }

  handlePageEvent(event: any) {
    this.pageCount = event.pageSize;
    this.pageNumber = event.pageIndex;

    this.GetAll();
  }

  delete(doctorId: number) {
    let confirmation = confirm(
      `Are you sure you want to remove the doctor (ID: ${doctorId})?`
    );

    if (confirmation) {
      let ids = [doctorId];

      this.httpService.Delete(ids).subscribe((response: any) => {
        this.toastr.success('Doctor removed succesfully', 'Confirmation');

        this.GetAll();
      });
    }
  }

  createDoctor() {
    const dialogRef = this.dialog.open(FormComponent, {
      disableClose: true,
      autoFocus: true,
      closeOnNavigation: false,
      position: { top: '30px' },
      width: '700px',
      data: {
        type: 'Create',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== false) {
        this.toastr.success('Doctor created succesfully', 'Confirmation');
        this.GetAll();
      }
    });
  }
}
