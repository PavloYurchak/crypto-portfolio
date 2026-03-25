import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { UserProfile } from '../../core/models/user.models';
import { UserService } from '../../core/services/user.service';

@Component({
  selector: 'app-profile',
  imports: [CommonModule, MatButtonModule, MatCardModule, MatProgressSpinnerModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  private readonly userService = inject(UserService);

  isLoading = true;
  error = '';
  profile: UserProfile | null = null;

  constructor() {
    this.loadProfile();
  }

  reload(): void {
    this.loadProfile();
  }

  private loadProfile(): void {
    this.isLoading = true;
    this.error = '';

    this.userService.getCurrentUser().subscribe({
      next: (profile) => {
        this.profile = profile;
        this.isLoading = false;
      },
      error: (error: unknown) => {
        if (error instanceof HttpErrorResponse) {
          this.error = (error.error as { error?: string })?.error || 'Failed to load profile.';
        } else {
          this.error = error instanceof Error ? error.message : 'Failed to load profile.';
        }

        this.profile = null;
        this.isLoading = false;
      }
    });
  }
}
