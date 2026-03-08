import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

import { environment } from '../../../environments/environment';
import { ApiResponse } from '../models/auth.models';
import { UserProfile } from '../models/user.models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly apiUrl = `${environment.apiBaseUrl}/Users`;

  constructor(private readonly http: HttpClient) {}

  getCurrentUser(): Observable<UserProfile> {
    return this.http.get<ApiResponse<UserProfile>>(`${this.apiUrl}/me`).pipe(
      map((response) => {
        if (!response.success || !response.result) {
          throw new Error(response.error || 'Unable to load user profile');
        }

        return response.result;
      })
    );
  }
}
