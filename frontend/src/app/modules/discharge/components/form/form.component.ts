import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../global/shared.module';
import { HttpService } from '../../../../core/services/http.service';
import { AdmissionService } from '../../../../core/services/admission.service';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss',
})
export class FormComponent implements OnInit {
  formGroup!: FormGroup;
  admissions: any[] = [];

  readonly dialogRef = inject(MatDialogRef<FormComponent>);
  data = inject(MAT_DIALOG_DATA);

  constructor(private fb: FormBuilder, private httpService: HttpService, private admissionService: AdmissionService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadAdmissions();
  }

  initForm() {
    this.formGroup = this.fb.group({
      amissionId: [null, [Validators.required]],
      dischargeDate: [null, [Validators.required]],
      Treatment: ['', [Validators.required]],
      Amount: [0, [Validators.required]],
      IsPaid: ['', [Validators.required]],
    });
  }

  loadAdmissions() {
    this.admissionService.getAll(100, 0, '').subscribe(
      (response: any) => {
        this.admissions = response.data;
      }
    );
  }

  cancel() {
    this.dialogRef.close(false);
  }

  save() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;
      this.admissionService
        .CreateAdmission(
          formData.patientId,
          formData.doctorId,
          formData.admissionDate,
          formData.diagnosis,
          formData.observation
        )
        .subscribe(() => {
          this.dialogRef.close(true);
        });
    } else {
      this.markFormGroupTouched(this.formGroup);
    }
  }

  markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) => {
      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      } else {
        control.markAsTouched();
      }
    });
  }
}
