import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../shared.module';
import { AuthService } from '../../../../core/services/auth.service';
import { HttpService } from '../../../../core/services/http.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, SharedModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private httpService: HttpService
  ) {}

  onLogin() {
    this.authService.login(this.email, this.password).subscribe({
      next: (response: any) => {

        if (response && response.accessToken) {
          localStorage.setItem('accessToken', response.accessToken);
          this.router.navigate(['']); 
        } else {
          console.error('Error: No accessToken received');
          alert('Invalid login credentials');
        }
      },
      error: (error) => {
        console.error('Login failed', error);
        alert('Invalid login credentials');
      },
    });
  }
}
