import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedModule } from '../../../global/shared.module';

@Component({
  selector: 'app-details-dialog',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './details-dialog.component.html',
  styleUrl: './details-dialog.component.scss'
})
export class DetailsDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}

}
