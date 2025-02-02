import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../global/shared.module';
import { PatientService } from '../../../../core/services/patient.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-form',
  imports: [SharedModule],
  templateUrl: './form.component.html',
})
export class FormComponent implements OnInit {
  formGroup!: FormGroup;
  readonly dialogRef = inject(MatDialogRef<FormComponent>);
  data = inject(MAT_DIALOG_DATA);

  constructor(
    private patientService: PatientService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  cancel() {
    this.dialogRef.close(false);
  }

  save() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;

      this.patientService.create(formData).subscribe({
        next: () => {
          this.toastr.success('Patient created', 'Success');
          this.dialogRef.close(formData);
        },
        error: (error) => {
          console.error('Error:', error);
          this.toastr.error('Error creating patient', 'Error');
        },
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
      address: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      observations: ['', Validators.required],
    });
  }
}
