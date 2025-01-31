import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss',
})
export class IndexComponent implements OnInit {
  role: string | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {

    this.role = this.authService.getUserRole();
  }
}
