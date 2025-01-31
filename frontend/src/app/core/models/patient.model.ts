export interface Patient {
  id: number;
  firstName: string;
  lastName: string;
  address: string;
  phoneNumber: string;
  observations?: string;
}

export interface CreatePatientRequestDto {
  firstName: string;
  lastName: string;
  address: string;
  phoneNumber: string;
  observations?: string;
}

export interface UpdatePatientRequestDto {
  firstName: string;
  lastName: string;
  address: string;
  phoneNumber: string;
  observations?: string;
}
