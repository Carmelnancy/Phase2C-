import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { AssetRequest } from '../models/assetrequest.model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private baseUrl = 'http://localhost:5239/api/v1/AssetRequest';

  constructor(private http: HttpClient) {}

  getMyRequests(): Observable<AssetRequest[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<AssetRequest[]>(`${this.baseUrl}/myrequests`,{headers});
  }
  createRequest(req: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  return this.http.post(`${this.baseUrl}/CreateAssetRequest`, req,{headers});
  }

}
