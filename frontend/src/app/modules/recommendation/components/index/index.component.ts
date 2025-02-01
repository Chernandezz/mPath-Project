import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../../global/shared.module';
import { Recommendation } from '../../../../core/models/recommendation.model';
import { DischargeService } from '../../../../core/services/discharge.service';
import { Discharge } from '../../../../core/models/discharge.model';
import { RecommendationService } from '../../../../core/services/recommendation.service';

@Component({
  selector: 'app-index',
  imports: [SharedModule],
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
})
export class IndexComponent implements OnInit {
  displayedColumns = ['id', 'dischargeDate', 'recommendation', 'actions'];
  dataSource = new MatTableDataSource<Discharge>([]);
  totalItems = 0;
  pageCount = 10;
  pageNumber = 0;
  searchText = '';
  userId = Number(localStorage.getItem('userId'));
  paginationOptions: number[] = [5, 10, 25, 50];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private toastr: ToastrService,
    public dialog: MatDialog,
    private dischargeService: DischargeService,
    private recommendationService: RecommendationService
  ) {}

  ngOnInit(): void {
    this.loadRecommendations();
  }

  loadRecommendations() {
    this.dischargeService
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

  handlePageEvent(event: PageEvent) {
    this.pageCount = event.pageSize;
    this.pageNumber = event.pageIndex;
    this.loadRecommendations();
  }
  activateRecommendation(recommendationId: number) {    
    this.recommendationService
      .activateRecommendation(recommendationId)
      .subscribe(() => {
        this.loadRecommendations();
      });
  }

  deactivateRecommendation(recommendationId: number) {
    this.recommendationService
      .deactivateRecommendation(recommendationId)
      .subscribe(() => {
        this.loadRecommendations();
      });
  }
}
