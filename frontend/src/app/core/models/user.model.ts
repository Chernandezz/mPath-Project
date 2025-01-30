export interface LoginRequestDto {
  email: string;
  password: string;
}

export interface LoginResponseDto {
  email: string;
  accessToken: string;
  expiresIn: number;
}
