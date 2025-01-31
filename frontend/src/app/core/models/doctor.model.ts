export interface Doctor {
  Id: number;
  FirstName: string;
  LastName: string;
  Active: boolean;
}

export interface CreateDoctorRequestDto {
  FirstName: string;
  LastName: string;
  Active: boolean;
}

export interface UpdateDoctorRequestDto {
  FirstName: string;
  LastName: string;
  Active: boolean;
}
