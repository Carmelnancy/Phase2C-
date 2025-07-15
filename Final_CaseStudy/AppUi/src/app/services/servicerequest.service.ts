import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { ServiceRequest } from '../models/servicerequest.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceRequestService {
  private baseUrl = 'http://localhost:5239/api/v1/ServiceRequest';

  constructor(private http: HttpClient) {}

  getRequestsByEmployee(): Observable<ServiceRequest[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<ServiceRequest[]>(`${this.baseUrl}/myrequests`,{headers});
  }
  createServiceRequest(req: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  return this.http.post(`${this.baseUrl}/AddServiceRequest`, req,{headers});
}

}
