import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../shared.module';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  formGroup!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.formGroup = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onLogin() {
    
    if (this.formGroup.invalid) {
      this.toastr.warning(
        'Please complete all required fields',
        'Validation Error',
        {
          closeButton: true,
          timeOut: 3000,
          progressBar: true,
        }
      );
      return;
    }
    const { email, password } = this.formGroup.value;

    this.authService.login(email, password).subscribe({
      next: (response: any) => {
        if (response?.accessToken) {
          localStorage.setItem('accessToken', response.accessToken);
          this.router.navigate(['']);
        } else {
          this.toastr.error('Invalid login credentials', 'Login Error');
        }
      },
      error: (error) => {
        console.error('Login failed', error);

        if (error.status === 401) {
          this.toastr.error('Incorrect username or password', 'Login Error');
        } else {
          this.toastr.error('An unexpected error occurred', 'Login Error');
        }
      },
    });
  }
}
