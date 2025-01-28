import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpService } from '../../../../services/http.service';
import { SharedModule } from '../../../global/shared.module';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss',
})
export class FormComponent implements OnInit {
  formGroup!: FormGroup;
  doctors: any[] = []; // Lista de doctores para el dropdown
  patients: any[] = []; // Lista de pacientes para el dropdown

  readonly dialogRef = inject(MatDialogRef<FormComponent>);
  data = inject(MAT_DIALOG_DATA);

  constructor(private fb: FormBuilder, private httpService: HttpService) {}

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
    this.httpService.GetAll(100, 0, '', 'Doctor').subscribe((response: any) => {
      this.doctors = response.data;
    });
  }

  loadPatients() {
    this.httpService
      .GetAll(100, 0, '', 'Patient')
      .subscribe((response: any) => {
        console.log(response.data);

        this.patients = response.data;
      });
  }

  cancel() {
    this.dialogRef.close(false);
  }

  save() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;
      this.httpService
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
