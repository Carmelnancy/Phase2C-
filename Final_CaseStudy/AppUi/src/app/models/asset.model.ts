export interface Asset {
  assetId: number;
  assetName: string;
  quantity: number;
  status: string;
  manufacturingDate: Date;
  expiryDate?: Date;
}
