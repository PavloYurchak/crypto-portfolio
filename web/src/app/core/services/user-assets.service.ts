import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

import { environment } from '../../../environments/environment';
import { ApiResponse } from '../models/auth.models';
import { AssetReference, CurrencyReference } from '../models/reference.models';
import { UpsertUserAssetRequest, UserAsset } from '../models/user-asset.models';

@Injectable({
  providedIn: 'root'
})
export class UserAssetsService {
  private readonly userAssetsApiUrl = `${environment.apiBaseUrl}/UserAssets`;
  private readonly assetsApiUrl = `${environment.apiBaseUrl}/Assets`;
  private readonly currenciesApiUrl = `${environment.apiBaseUrl}/Currencies`;

  constructor(private readonly http: HttpClient) {}

  getUserAssets(): Observable<UserAsset[]> {
    return this.http
      .get<unknown>(`${this.userAssetsApiUrl}/user-assets`)
      .pipe(
        map((payload) => {
          const result = this.unwrap<unknown>(payload, 'Unable to load user assets');
          const rows = this.toRows(result);
          return rows.map((row) => this.mapUserAsset(row));
        })
      );
  }

  createUserAsset(payload: UpsertUserAssetRequest): Observable<UserAsset> {
    return this.http
      .post<ApiResponse<UserAsset>>(`${this.userAssetsApiUrl}/user-asset`, payload)
      .pipe(map((response) => this.unwrap(response, 'Unable to add user asset')));
  }

  updateUserAsset(payload: UpsertUserAssetRequest): Observable<UserAsset> {
    return this.http
      .put<ApiResponse<UserAsset>>(`${this.userAssetsApiUrl}/user-asset`, payload)
      .pipe(map((response) => this.unwrap(response, 'Unable to update user asset')));
  }

  deleteUserAsset(assetId: number, currencyId: number): Observable<boolean> {
    return this.http
      .delete<ApiResponse<boolean>>(`${this.userAssetsApiUrl}/user-asset/${assetId}/${currencyId}`)
      .pipe(map((response) => this.unwrap(response, 'Unable to delete user asset')));
  }

  getAssets(): Observable<AssetReference[]> {
    return this.http
      .get<unknown>(`${this.assetsApiUrl}/assets`)
      .pipe(
        map((payload) => {
          const result = this.unwrap<unknown>(payload, 'Unable to load assets');
          const rows = this.toRows(result);
          return rows.map((row) => this.mapAssetReference(row));
        })
      );
  }

  getCurrencies(): Observable<CurrencyReference[]> {
    return this.http
      .get<unknown>(`${this.currenciesApiUrl}/currencies`)
      .pipe(
        map((payload) => {
          const result = this.unwrap<unknown>(payload, 'Unable to load currencies');
          const rows = this.toRows(result);
          return rows.map((row) => this.mapCurrencyReference(row));
        })
      );
  }

  private unwrap<T>(payload: unknown, fallbackError: string): T {
    if (!this.isApiResponse(payload)) {
      return payload as T;
    }

    const response = payload as ApiResponse<T>;
    if (!response.success || response.result == null) {
      throw new Error(response.error || fallbackError);
    }

    return response.result;
  }

  private isApiResponse(payload: unknown): payload is ApiResponse<unknown> {
    if (!payload || typeof payload !== 'object') {
      return false;
    }

    return typeof (payload as Record<string, unknown>)['success'] === 'boolean';
  }

  private toRows(value: unknown): unknown[] {
    if (value == null) {
      return [];
    }

    if (typeof value === 'string') {
      try {
        return this.toRows(JSON.parse(value));
      } catch {
        return [];
      }
    }

    if (Array.isArray(value)) {
      return value;
    }

    if (typeof value === 'object') {
      const obj = value as Record<string, unknown>;
      const nested = obj['$values'] ?? obj['items'] ?? obj['Items'] ?? obj['data'] ?? obj['Data'];

      if (Array.isArray(nested)) {
        return nested;
      }

      if (nested != null && typeof nested === 'object') {
        return [nested];
      }

      return [obj];
    }

    return [];
  }

  private mapUserAsset(raw: unknown): UserAsset {
    const item = (raw ?? {}) as Record<string, unknown>;

    return {
      id: this.toNumber(item['id'] ?? item['Id']),
      userId: this.toNumber(item['userId'] ?? item['UserId']),
      assetId: this.toNumber(item['assetId'] ?? item['AssetId']),
      currencyId: this.toNumber(item['currencyId'] ?? item['CurrencyId']),
      assetSymbol: this.toString(item['assetSymbol'] ?? item['AssetSymbol']),
      assetName: this.toString(item['assetName'] ?? item['AssetName']),
      currencySymbol: this.toString(item['currencySymbol'] ?? item['CurrencySymbol']),
      quantity: this.toNumber(item['quantity'] ?? item['Quantity'])
    };
  }

  private mapAssetReference(raw: unknown): AssetReference {
    const item = (raw ?? {}) as Record<string, unknown>;

    return {
      id: this.toNumber(item['id'] ?? item['Id']),
      symbol: this.toString(item['symbol'] ?? item['Symbol']),
      name: this.toString(item['name'] ?? item['Name'])
    };
  }

  private mapCurrencyReference(raw: unknown): CurrencyReference {
    const item = (raw ?? {}) as Record<string, unknown>;

    return {
      id: this.toNumber(item['id'] ?? item['Id']),
      symbol: this.toString(item['symbol'] ?? item['Symbol']),
      name: this.toString(item['name'] ?? item['Name'])
    };
  }

  private toNumber(value: unknown): number {
    if (typeof value === 'number') {
      return value;
    }

    if (typeof value === 'string') {
      const parsed = Number(value);
      return Number.isFinite(parsed) ? parsed : 0;
    }

    return 0;
  }

  private toString(value: unknown): string {
    return typeof value === 'string' ? value : '';
  }
}
