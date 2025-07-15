import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Employee } from '../models/employee.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private baseUrl = 'http://localhost:5239/api/v1/Employee';

  constructor(private http: HttpClient) { }

  //  Get employee profile (used in dashboard)
  getProfile(): Observable<Employee> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Employee>(`${this.baseUrl}/profile`, { headers });
  }

  // Get assets assigned to logged-in employee
  getMyAssets(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const employeeId = Number(localStorage.getItem('employeeId'));
    return this.http.get<any[]>(`http://localhost:5239/api/v1/Asset/GetAssetByEmpId/${employeeId}`, { headers });
  }
  registerEmployee(employee: any) {
    return this.http.post(`${this.baseUrl}/registration`, employee);
  }

  getEmployeeById(id: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.get(`${this.baseUrl}/GetEmployeeById/${id}`, { headers });
  }

  updateEmployee(emp: any): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.baseUrl}/UpdateEmployee`, emp,{ headers });
  }
  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      Authorization: `Bearer ${token}`
    };
  }


}
