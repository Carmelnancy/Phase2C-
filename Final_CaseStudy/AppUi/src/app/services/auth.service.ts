import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtResponse } from '../models/jwtresponse.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'http://localhost:5239/api/v1/Employee'; // Change if needed

  constructor(private http: HttpClient) { }

  // login(credentials: any): Observable<JwtResponse> {
  //   return this.http.post<JwtResponse>(`${this.baseUrl}/login`, credentials);
  // }

  login(data: { email: string; password: string }): Observable<any> {
  return this.http.post(`${this.baseUrl}/login`, data);
}
  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }

  getEmployeeId(): number {
    return Number(localStorage.getItem('employeeId'));
  }

  getUserRole(): string | null {
    return localStorage.getItem('role');
  }

  // storeSessionDetails(response: JwtResponse) {
  //   this.saveToken(response.token);
  //   localStorage.setItem('employeeId', response.employeeId.toString());
  //   localStorage.setItem('role', response.role);
  // }

  storeSessionDetails(response: any) {
  this.saveToken(response.token);
  // Decode token to extract employeeId and role
  const tokenPayload = JSON.parse(atob(response.token.split('.')[1]));
  localStorage.setItem('employeeId', tokenPayload.employeeId);
  localStorage.setItem('role', tokenPayload.role);
  }

//   storeSessionDetails(response: any) {
//   this.saveToken(response.token);

//   const tokenPayload = JSON.parse(atob(response.token.split('.')[1]));

//   // Save employeeId (this is correct)
//   localStorage.setItem('employeeId', tokenPayload.employeeId?.tostring());

//   //  Extract role from JWT claim
//   const roleClaim = tokenPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
//   if (roleClaim) {
//     localStorage.setItem('role', roleClaim);
//   }
// }


}
