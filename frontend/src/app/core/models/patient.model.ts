export interface Patient {
  id: number;
  firstName: string;
  lastName: string;
  address: string;
  phoneNumber: string;
  observations?: string;
}
