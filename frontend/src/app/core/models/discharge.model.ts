export interface Discharge {
  id: number;
  treatment: string;
  dischargeDate: string;
  amount: number;
  isPaid: boolean;
  admissionId: number;
}

export interface CreateDischargeRequestDto {
  treatment: string;
  dischargeDate: string;
  amount: number;
  isPaid: boolean;
  admissionId: number;
}
