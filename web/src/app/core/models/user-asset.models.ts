export interface UserAsset {
  id: number;
  userId: number;
  assetId: number;
  currencyId: number;
  assetSymbol: string;
  assetName: string;
  currencySymbol: string;
  quantity: number;
}

export interface UpsertUserAssetRequest {
  assetId: number;
  currencyId: number;
  quantity: number;
  price: number;
  executedAt: string | null;
}
