import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DoctorService } from '../../../../core/services/doctor.service';
import { SharedModule } from '../../../global/shared.module';

@Component({
  selector: 'app-form',
  imports: [SharedModule],
  templateUrl: './form.component.html',
})
export class FormComponent implements OnInit {
  formGroup!: FormGroup;
  readonly dialogRef = inject(MatDialogRef<FormComponent>);
  data = inject(MAT_DIALOG_DATA);

  constructor(private doctorService: DoctorService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initForm();
  }

  cancel() {
    this.dialogRef.close(false);
  }

  save() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;

      this.doctorService.create(formData).subscribe({
        next: () => this.dialogRef.close(formData),
        error: (error) => console.error('Error:', error),
      });
    } else {
      this.markFormGroupTouched(this.formGroup);
    }
  }

  private markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) =>
      control.markAsTouched()
    );
  }

  private initForm() {
    this.formGroup = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      active: [true, Validators.required],
    });
  }
}
