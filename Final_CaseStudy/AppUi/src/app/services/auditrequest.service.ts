import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { AuditRequest } from '../models/auditrequest.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuditRequestService {
  private baseUrl = 'http://localhost:5239/api/v1/AuditRequest';

  constructor(private http: HttpClient) {}

//   getAuditHistoryByEmployee(employeeId: number): Observable<AuditRequest[]> {
//     return this.http.get<AuditRequest[]>(`${this.baseUrl}/employee/${employeeId}`);
//   }
  getMyAuditRequests(): Observable<AuditRequest[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  return this.http.get<AuditRequest[]>(`${this.baseUrl}/myrequests`,{headers});
}

}
