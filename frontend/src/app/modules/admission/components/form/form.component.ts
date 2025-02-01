import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../global/shared.module';
import { HttpService } from '../../../../core/services/http.service';
import { DoctorService } from '../../../../core/services/doctor.service';
import { AdmissionService } from '../../../../core/services/admission.service';
import { PatientService } from '../../../../core/services/patient.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss',
})
export class FormComponent implements OnInit {
  formGroup!: FormGroup;
  doctors: any[] = [];
  patients: any[] = [];

  readonly dialogRef = inject(MatDialogRef<FormComponent>);
  data = inject(MAT_DIALOG_DATA);

  constructor(
    private fb: FormBuilder,
    private httpService: HttpService,
    private httpDoctor: DoctorService,
    private httpAdmission: AdmissionService,
    private httpPatient: PatientService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadDoctors();
    this.loadPatients();
  }

  initForm() {
    this.formGroup = this.fb.group({
      patientId: [null, [Validators.required]],
      doctorId: [null, [Validators.required]],
      admissionDate: [null, [Validators.required]],
      diagnosis: ['', [Validators.required]],
      observation: ['', [Validators.required]],
    });
  }

  loadDoctors() {
    this.httpDoctor.getAll(100, 0, '').subscribe((response: any) => {
      this.doctors = response.data;
    });
  }

  loadPatients() {
    this.httpPatient.getAll(100, 0, '').subscribe((response: any) => {
      this.patients = response.data;
    });
  }

  cancel() {
    this.dialogRef.close(false);
  }

  save() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;
      this.httpAdmission.create(formData).subscribe({
        next: () => {
          this.toastr.success('Admission created', 'Success');
          this.dialogRef.close(formData);
        },
        error: (error) => {
          console.error('Error:', error);
          this.toastr.error('Error creating admission', 'Error');
        },
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

