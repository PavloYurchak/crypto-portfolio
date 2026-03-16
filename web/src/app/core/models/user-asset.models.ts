export interface UserAsset {
  id: number;
  userId: number;
  assetId: number;
  currencyId: number;
  assetSymbol: string;
  assetName: string;
  currencySymbol: string;
  quantity: number;
  price: number | null;
  executedAt: string | null;
}

export interface UpsertUserAssetRequest {
  assetId: number;
  currencyId: number;
  quantity: number;
  price: number;
  executedAt: string | null;
}

export interface UserAssetTransaction {
  id: number;
  assetId: number;
  currencyId: number;
  quantity: number;
  price: number;
  executedAt: string | null;
}
