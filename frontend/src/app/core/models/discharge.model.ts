export interface Discharge {
  id: number;
  treatment: string;
  dischargeDate: string;
  amount: number;
  isPaid: boolean;
  admissionId: number;
  recommendation: string;
  completed: boolean;
}

export interface CreateDischargeRequestDto {
  treatment: string;
  dischargeDate: string;
  amount: number;
  isPaid: boolean;
  admissionId: number;
}
