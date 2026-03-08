import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, tap } from 'rxjs';

import { environment } from '../../../environments/environment';
import { ApiResponse, AuthResult, LoginRequest } from '../models/auth.models';

const ACCESS_TOKEN_KEY = 'cp_access_token';
const REFRESH_TOKEN_KEY = 'cp_refresh_token';
const USER_NAME_KEY = 'cp_user_name';
const USER_EMAIL_KEY = 'cp_user_email';
const TOKEN_EXPIRES_AT_KEY = 'cp_token_expires_at';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = `${environment.apiBaseUrl}/Auth`;

  constructor(private readonly http: HttpClient) {}

  login(payload: LoginRequest): Observable<AuthResult> {
    return this.http.post<ApiResponse<AuthResult>>(`${this.apiUrl}/login`, payload).pipe(
      map((response) => {
        if (!response.success || !response.result) {
          throw new Error(response.error || 'Login failed');
        }

        return response.result;
      }),
      tap((result) => this.persistSession(result))
    );
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem(REFRESH_TOKEN_KEY);
    localStorage.removeItem(USER_NAME_KEY);
    localStorage.removeItem(USER_EMAIL_KEY);
    localStorage.removeItem(TOKEN_EXPIRES_AT_KEY);
  }

  isAuthenticated(): boolean {
    const token = this.getAccessToken();
    const expiresAt = localStorage.getItem(TOKEN_EXPIRES_AT_KEY);

    if (!token || !expiresAt) {
      return false;
    }

    return new Date(expiresAt).getTime() > Date.now();
  }

  getAccessToken(): string | null {
    return localStorage.getItem(ACCESS_TOKEN_KEY);
  }

  getUserName(): string {
    return localStorage.getItem(USER_NAME_KEY) || 'User';
  }

  private persistSession(result: AuthResult): void {
    localStorage.setItem(ACCESS_TOKEN_KEY, result.accessToken);
    localStorage.setItem(REFRESH_TOKEN_KEY, result.refreshToken);
    localStorage.setItem(USER_NAME_KEY, result.userName);
    localStorage.setItem(USER_EMAIL_KEY, result.email);
    localStorage.setItem(TOKEN_EXPIRES_AT_KEY, result.accessTokenExpiresAt);
  }
}
