export interface Admission {
  id: number;
  patientName: string;
  admissionDate: string;
  diagnosis: string;
  observation: string;
  doctorId: number;
  patientId: number;
}

export interface CreateAdmissionRequestDto {
  patientId: number;
  doctorId: number;
  admissionDate: string;
  diagnosis: string;
  observation?: string;
}
