import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpService } from '../../../../services/http.service';
import { SharedModule } from '../../shared.module';

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
    this.httpService.login(this.email, this.password).subscribe({
      next: (response: any) => {
        localStorage.setItem('token', response.data.Token);
        this.router.navigate(['']); // Redirige a una ruta protegida
      },
      error: (error) => {
        console.error('Login failed', error);
        alert('Invalid login credentials');
      },
    });
  }
}
