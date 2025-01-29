import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { SharedModule } from '../../../global/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpService } from '../../../../services/http.service';
import { IndexComponent } from '../index/index.component';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss',
})
export class FormComponent implements OnInit {
  formGroup!: FormGroup;

  readonly dialogRef = inject(MatDialogRef<FormComponent>);
  data = inject(MAT_DIALOG_DATA);

  constructor(private httpService: HttpService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initForm();
  }

  cancel() {
    this.dialogRef.close(false);
  }
  
  save() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;

     
      this.httpService
        .CreateDoctor(
          formData.id, 
          formData.firstName,
          formData.lastName,
          formData.active,
          formData.email
        )
        .subscribe({
          next: (response: any) => {
            this.dialogRef.close(formData); 
          },
          error: (error: any) => {
            console.error('Error sending data to backend:', error);
          },
        });
    } else {
      this.markFormGroupTouched(this.formGroup);
    }
  }

  markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) => {
      control.markAsTouched();

      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }

  initForm() {
    this.formGroup = this.fb.group({
      firstName: [{ value: '', disabled: false }, [Validators.required]],
      lastName: [{ value: '', disabled: false }, [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      active: [true, [Validators.required]],
    });
  }
}
