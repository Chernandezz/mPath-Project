import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { Router, RouterOutlet } from '@angular/router';
import { HttpService } from '../../../../services/http.service';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [SharedModule, RouterOutlet],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  role: string | null = null;
  constructor(
    private router: Router,
    private httpService: HttpService,
    private authService: AuthService
  ) {}

  navigateBasedOnAuth() {
    if (this.httpService.isLoggedIn()) {
      this.router.navigate(['/login']);
    } else {
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.role = this.authService.getUserRole();
  }

  logout() {
    this.httpService.logout();
    this.router.navigate(['/login']);
  }
}
