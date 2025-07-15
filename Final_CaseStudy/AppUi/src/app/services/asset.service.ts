import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Asset } from '../models/asset.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssetService {
  private baseUrl = 'http://localhost:5239/api/v1/Asset';

  constructor(private http: HttpClient) {}

  //  Get all assets (employee will see this for selection)
  getAllAssets(): Observable<Asset[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Asset[]>(`${this.baseUrl}/GetAllAssets`,{headers});
  }

  //  Get asset by ID (optional usage)
  getAssetById(assetId: number): Observable<Asset> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Asset>(`${this.baseUrl}/GetAssetById/${assetId}`,{headers});
  }

  // (Optional) Get only Available assets
  getAssetsByStatus(status: string): Observable<Asset[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Asset[]>(`${this.baseUrl}/GetAssetByStatus/${status}`,{headers});
  }
}
