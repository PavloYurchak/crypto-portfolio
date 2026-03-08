export interface ApiResponse<T> {
  success: boolean;
  error?: string | null;
  statusCode?: number | null;
  result?: T | null;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResult {
  userId: number;
  email: string;
  userName: string;
  accessToken: string;
  accessTokenExpiresAt: string;
  refreshToken: string;
}
