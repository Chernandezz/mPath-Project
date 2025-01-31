export interface Patient {
  Id: number;
  FirstName: string;
  LastName: string;
  Address: string;
  PhoneNumber: string;
  Observations?: string;
}

export interface CreatePatientRequestDto {
  FirstName: string;
  LastName: string;
  Address: string;
  PhoneNumber: string;
  Observations?: string;
}

export interface UpdatePatientRequestDto {
  FirstName: string;
  LastName: string;
  Address: string;
  PhoneNumber: string;
  Observations?: string;
}
