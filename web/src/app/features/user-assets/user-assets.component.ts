import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';

import { AssetReference, CurrencyReference } from '../../core/models/reference.models';
import { UpsertUserAssetRequest, UserAsset } from '../../core/models/user-asset.models';
import { UserAssetsService } from '../../core/services/user-assets.service';

@Component({
  selector: 'app-user-assets',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatToolbarModule,
    RouterLink
  ],
  templateUrl: './user-assets.component.html',
  styleUrl: './user-assets.component.scss'
})
export class UserAssetsComponent {
  private readonly fb = inject(FormBuilder);
  private readonly userAssetsService = inject(UserAssetsService);

  isLoading = true;
  isSubmitting = false;
  loadError = '';
  submitError = '';
  successMessage = '';
  filterQuery = '';

  assets: AssetReference[] = [];
  currencies: CurrencyReference[] = [];
  userAssets: UserAsset[] = [];

  editAssetId: number | null = null;
  editCurrencyId: number | null = null;
  deletingKey = '';

  readonly form = this.fb.nonNullable.group({
    assetId: [0, [Validators.required, Validators.min(1)]],
    currencyId: [0, [Validators.required, Validators.min(1)]],
    quantity: [0, [Validators.required, Validators.min(0.00000001)]],
    price: [0, [Validators.required, Validators.min(0)]],
    executedAt: ['']
  });

  constructor() {
    this.loadInitialData();
  }

  get isEditing(): boolean {
    return this.editAssetId !== null && this.editCurrencyId !== null;
  }

  get filteredUserAssets(): UserAsset[] {
    const query = this.filterQuery.trim().toLowerCase();
    const items = [...this.userAssets].sort((a, b) => {
      const left = `${a.assetSymbol} ${a.currencySymbol}`.toLowerCase();
      const right = `${b.assetSymbol} ${b.currencySymbol}`.toLowerCase();
      return left.localeCompare(right);
    });

    if (!query) {
      return items;
    }

    return items.filter((item) =>
      `${item.assetSymbol} ${item.assetName} ${item.currencySymbol}`.toLowerCase().includes(query)
    );
  }

  get totalQuantity(): number {
    return this.filteredUserAssets.reduce((sum, row) => sum + row.quantity, 0);
  }

  get hasUserAssets(): boolean {
    return this.userAssets.length > 0;
  }

  submit(): void {
    if (this.form.invalid || this.isSubmitting) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitError = '';
    this.successMessage = '';
    this.isSubmitting = true;

    const payload = this.toPayload();
    const request$ = this.isEditing
      ? this.userAssetsService.updateUserAsset(payload)
      : this.userAssetsService.createUserAsset(payload);

    request$.subscribe({
      next: () => {
        this.successMessage = this.isEditing ? 'User asset updated.' : 'User asset added.';
        this.cancelEdit();
        this.reloadUserAssets();
      },
      error: (error: unknown) => {
        this.submitError = this.extractError(error, 'Failed to save user asset.');
        this.isSubmitting = false;
      }
    });
  }

  edit(item: UserAsset): void {
    this.editAssetId = item.assetId;
    this.editCurrencyId = item.currencyId;
    this.form.patchValue({
      assetId: item.assetId,
      currencyId: item.currencyId,
      quantity: item.quantity,
      price: 0,
      executedAt: ''
    });
    this.form.controls.assetId.disable();
    this.form.controls.currencyId.disable();
    this.submitError = '';
    this.successMessage = '';
  }

  cancelEdit(): void {
    this.editAssetId = null;
    this.editCurrencyId = null;
    this.form.reset({
      assetId: 0,
      currencyId: 0,
      quantity: 0,
      price: 0,
      executedAt: ''
    });
    this.form.controls.assetId.enable();
    this.form.controls.currencyId.enable();
    this.isSubmitting = false;
  }

  remove(item: UserAsset): void {
    if (!confirm(`Delete ${item.assetSymbol}/${item.currencySymbol} from your assets?`)) {
      return;
    }

    const key = this.toRowKey(item.assetId, item.currencyId);
    if (this.deletingKey) {
      return;
    }

    this.deletingKey = key;
    this.submitError = '';
    this.successMessage = '';

    this.userAssetsService.deleteUserAsset(item.assetId, item.currencyId).subscribe({
      next: () => {
        this.successMessage = 'User asset deleted.';
        this.deletingKey = '';
        this.userAssets = this.userAssets.filter((row) => this.toRowKey(row.assetId, row.currencyId) !== key);
      },
      error: (error: unknown) => {
        this.submitError = this.extractError(error, 'Failed to delete user asset.');
        this.deletingKey = '';
      }
    });
  }

  retryLoad(): void {
    this.loadInitialData();
  }

  refreshList(): void {
    this.submitError = '';
    this.successMessage = '';
    this.reloadUserAssets();
  }

  isDeleting(assetId: number, currencyId: number): boolean {
    return this.deletingKey === this.toRowKey(assetId, currencyId);
  }

  trackByUserAsset(_: number, item: UserAsset): string {
    return this.toRowKey(item.assetId, item.currencyId);
  }

  private loadInitialData(): void {
    this.isLoading = true;
    this.loadError = '';

    forkJoin({
      assets: this.userAssetsService.getAssets(),
      currencies: this.userAssetsService.getCurrencies(),
      userAssets: this.userAssetsService.getUserAssets()
    }).subscribe({
      next: ({ assets, currencies, userAssets }) => {
        this.assets = assets;
        this.currencies = currencies;
        this.userAssets = userAssets;
        this.isLoading = false;
      },
      error: (error: unknown) => {
        this.loadError = this.extractError(error, 'Failed to load user assets page.');
        this.isLoading = false;
      }
    });
  }

  private reloadUserAssets(): void {
    this.userAssetsService.getUserAssets().subscribe({
      next: (userAssets) => {
        this.userAssets = userAssets;
        this.successMessage ||= 'List refreshed.';
        this.isSubmitting = false;
      },
      error: (error: unknown) => {
        this.submitError = this.extractError(error, 'Saved, but failed to refresh user assets list.');
        this.isSubmitting = false;
      }
    });
  }

  private toPayload(): UpsertUserAssetRequest {
    const value = this.form.getRawValue();
    const executedAt = value.executedAt ? new Date(value.executedAt).toISOString() : null;

    return {
      assetId: Number(value.assetId),
      currencyId: Number(value.currencyId),
      quantity: Number(value.quantity),
      price: Number(value.price),
      executedAt
    };
  }

  private toRowKey(assetId: number, currencyId: number): string {
    return `${assetId}-${currencyId}`;
  }

  private extractError(error: unknown, fallback: string): string {
    if (error instanceof HttpErrorResponse) {
      return (error.error as { error?: string })?.error || fallback;
    }

    return error instanceof Error ? error.message : fallback;
  }
}
