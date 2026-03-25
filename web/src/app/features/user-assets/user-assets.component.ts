import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, Injectable, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, MatNativeDateModule, NativeDateAdapter } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { RouterLink } from '@angular/router';
import { catchError, of, take, timeout } from 'rxjs';

import { AssetReference, CurrencyReference } from '../../core/models/reference.models';
import { UpsertUserAssetRequest, UserAsset, UserAssetTransaction } from '../../core/models/user-asset.models';
import { UserAssetsService } from '../../core/services/user-assets.service';

const DOT_DATE_FORMATS = {
  parse: {
    dateInput: 'input'
  },
  display: {
    dateInput: 'input',
    monthYearLabel: { year: 'numeric', month: 'short' },
    dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
    monthYearA11yLabel: { year: 'numeric', month: 'long' }
  }
};

@Injectable()
class DotDateAdapter extends NativeDateAdapter {
  override parse(value: unknown): Date | null {
    if (typeof value === 'string') {
      const trimmed = value.trim();
      const match = /^(\d{1,2})\.(\d{1,2})\.(\d{4})$/.exec(trimmed);
      if (match) {
        const day = Number(match[1]);
        const month = Number(match[2]);
        const year = Number(match[3]);
        const date = new Date(year, month - 1, day);
        return Number.isNaN(date.getTime()) ? null : date;
      }
    }

    return super.parse(value);
  }

  override format(date: Date, displayFormat: Object): string {
    if (displayFormat === 'input') {
      const day = `${date.getDate()}`.padStart(2, '0');
      const month = `${date.getMonth() + 1}`.padStart(2, '0');
      const year = date.getFullYear();
      return `${day}.${month}.${year}`;
    }

    return super.format(date, displayFormat);
  }
}

@Component({
  selector: 'app-user-assets',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    RouterLink
  ],
  providers: [
    { provide: DateAdapter, useClass: DotDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: DOT_DATE_FORMATS },
    { provide: MAT_DATE_LOCALE, useValue: 'uk-UA' }
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
  loadWarning = '';
  submitError = '';
  successMessage = '';
  filterQuery = '';
  assetQuery = '';
  currencyQuery = '';

  assets: AssetReference[] = [];
  currencies: CurrencyReference[] = [];
  userAssets: UserAsset[] = [];

  editAssetId: number | null = null;
  editCurrencyId: number | null = null;
  deletingKey = '';
  readonly hours = Array.from({ length: 24 }, (_, i) => `${i}`.padStart(2, '0'));
  readonly minutes = Array.from({ length: 60 }, (_, i) => `${i}`.padStart(2, '0'));

  readonly form = this.fb.nonNullable.group({
    assetId: [0, [Validators.required, Validators.min(1)]],
    currencyId: [0, [Validators.required, Validators.min(1)]],
    quantity: [0, [Validators.required, Validators.min(0.00000001)]],
    price: [0, [Validators.required, Validators.min(0)]],
    executedAt: ['' as string | Date],
    executedAtHour: [''],
    executedAtMinute: ['']
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

  get filteredAssets(): AssetReference[] {
    const query = this.assetQuery.trim().toLowerCase();
    if (!query) {
      return this.assets;
    }

    return this.assets.filter((asset) => `${asset.symbol} ${asset.name}`.toLowerCase().includes(query));
  }

  get filteredCurrencies(): CurrencyReference[] {
    const query = this.currencyQuery.trim().toLowerCase();
    if (!query) {
      return this.currencies;
    }

    return this.currencies.filter((currency) => `${currency.symbol} ${currency.name}`.toLowerCase().includes(query));
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
      price: item.price ?? 0,
      executedAt: this.toDateTimeLocalValue(item.executedAt),
      executedAtHour: this.toTimeParts(item.executedAt).hour,
      executedAtMinute: this.toTimeParts(item.executedAt).minute
    });
    this.form.controls.assetId.disable();
    this.form.controls.currencyId.disable();
    this.submitError = '';
    this.successMessage = '';

    if (item.price == null) {
      this.loadLatestPriceForItem(item, true);
    }
  }

  cancelEdit(): void {
    this.editAssetId = null;
    this.editCurrencyId = null;
    this.form.reset({
      assetId: 0,
      currencyId: 0,
      quantity: 0,
      price: 0,
      executedAt: '',
      executedAtHour: '',
      executedAtMinute: ''
    });
    this.form.controls.assetId.enable();
    this.form.controls.currencyId.enable();
    this.isSubmitting = false;
    this.assetQuery = '';
    this.currencyQuery = '';
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

  readonly trackByUserAsset = (_: number, item: UserAsset): string => `${item.assetId}-${item.currencyId}`;

  private loadInitialData(): void {
    this.isLoading = true;
    this.loadError = '';
    this.loadWarning = '';
    const warnings: string[] = [];
    let pending = 3;
    const done = (): void => {
      pending -= 1;
      if (pending > 0) {
        return;
      }

      if (warnings.length > 0) {
        this.loadWarning = warnings.join(' ');
      }
      if (warnings.length === 3) {
        this.loadError = 'Failed to load user assets page.';
      }
      this.isLoading = false;
    };

    this.userAssetsService
      .getAssets()
      .pipe(
        take(1),
        timeout(15000),
        catchError((error: unknown) => {
          warnings.push(this.extractError(error, 'Failed to load assets.'));
          return of([] as AssetReference[]);
        })
      )
      .subscribe({
        next: (assets) => {
          this.assets = assets;
        },
        complete: done
      });

    this.userAssetsService
      .getCurrencies()
      .pipe(
        take(1),
        timeout(15000),
        catchError((error: unknown) => {
          warnings.push(this.extractError(error, 'Failed to load currencies.'));
          return of([] as CurrencyReference[]);
        })
      )
      .subscribe({
        next: (currencies) => {
          this.currencies = currencies;
        },
        complete: done
      });

    this.userAssetsService
      .getUserAssets()
      .pipe(
        take(1),
        timeout(15000),
        catchError((error: unknown) => {
          warnings.push(this.extractError(error, 'Failed to load user assets.'));
          return of([] as UserAsset[]);
        })
      )
      .subscribe({
        next: (userAssets) => {
          this.userAssets = userAssets;
          this.loadLatestPrices(userAssets);
        },
        complete: done
      });
  }

  private reloadUserAssets(): void {
    this.userAssetsService.getUserAssets().subscribe({
      next: (userAssets) => {
        this.userAssets = userAssets;
        this.loadLatestPrices(userAssets);
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
    const executedAt = this.toIsoDate(value.executedAt, value.executedAtHour, value.executedAtMinute);

    return {
      assetId: Number(value.assetId),
      currencyId: Number(value.currencyId),
      quantity: Number(value.quantity),
      price: Number(value.price),
      executedAt
    };
  }

  private toIsoDate(value: unknown, hour: string, minute: string): string | null {
    if (!value) {
      return null;
    }

    const parsedHour = this.parseTwoDigit(hour, 0, 23);
    const parsedMinute = this.parseTwoDigit(minute, 0, 59);

    if (value instanceof Date) {
      if (Number.isNaN(value.getTime())) {
        return null;
      }

      const date = new Date(value);
      if (parsedHour !== null || parsedMinute !== null) {
        date.setHours(parsedHour ?? 0, parsedMinute ?? 0, 0, 0);
      }
      return date.toISOString();
    }

    if (typeof value === 'string') {
      const parsed = new Date(value);
      if (Number.isNaN(parsed.getTime())) {
        return null;
      }

      if (parsedHour !== null || parsedMinute !== null) {
        parsed.setHours(parsedHour ?? 0, parsedMinute ?? 0, 0, 0);
      }
      return parsed.toISOString();
    }

    return null;
  }

  private toRowKey(assetId: number, currencyId: number): string {
    return `${assetId}-${currencyId}`;
  }

  private loadLatestPrices(items: UserAsset[]): void {
    for (const item of items) {
      this.loadLatestPriceForItem(item, false);
    }
  }

  private loadLatestPriceForItem(item: UserAsset, patchEditForm: boolean): void {
    this.userAssetsService
      .getUserAssetTransactions(item.assetId, item.currencyId)
      .pipe(
        take(1),
        catchError(() => of([] as UserAssetTransaction[]))
      )
      .subscribe((transactions) => {
        const latest = this.pickLatestTransaction(transactions);
        if (!latest) {
          return;
        }

        item.price = latest.price;
        item.executedAt = latest.executedAt;

        if (patchEditForm && this.isEditing && this.editAssetId === item.assetId && this.editCurrencyId === item.currencyId) {
          this.form.patchValue({
            price: latest.price,
            executedAt: this.toDateTimeLocalValue(latest.executedAt),
            executedAtHour: this.toTimeParts(latest.executedAt).hour,
            executedAtMinute: this.toTimeParts(latest.executedAt).minute
          });
        }
      });
  }

  private pickLatestTransaction(transactions: UserAssetTransaction[]): UserAssetTransaction | null {
    if (!transactions.length) {
      return null;
    }

    return [...transactions].sort((a, b) => {
      const left = a.executedAt ? Date.parse(a.executedAt) : 0;
      const right = b.executedAt ? Date.parse(b.executedAt) : 0;
      if (left === right) {
        return b.id - a.id;
      }

      return right - left;
    })[0];
  }

  private toDateTimeLocalValue(value: string | null): Date | '' {
    if (!value) {
      return '';
    }

    const date = new Date(value);
    if (Number.isNaN(date.getTime())) {
      return '';
    }

    return date;
  }

  private toTimeParts(value: string | null): { hour: string; minute: string } {
    if (!value) {
      return { hour: '', minute: '' };
    }

    const date = new Date(value);
    if (Number.isNaN(date.getTime())) {
      return { hour: '', minute: '' };
    }

    const pad = (num: number): string => num.toString().padStart(2, '0');
    return {
      hour: pad(date.getHours()),
      minute: pad(date.getMinutes())
    };
  }

  private parseTwoDigit(value: string, min: number, max: number): number | null {
    if (!/^\d{2}$/.test((value || '').trim())) {
      return null;
    }

    const num = Number(value);
    return num >= min && num <= max ? num : null;
  }

  private extractError(error: unknown, fallback: string): string {
    if (error instanceof HttpErrorResponse) {
      return (error.error as { error?: string })?.error || fallback;
    }

    return error instanceof Error ? error.message : fallback;
  }
}
