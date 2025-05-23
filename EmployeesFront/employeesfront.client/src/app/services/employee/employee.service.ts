import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Employee } from '../../domain/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = `${environment.apiUrl}/Employees`;

  constructor(private http: HttpClient) { }

  getEmployees() {
    return this.http.get<Employee[]>(this.apiUrl);
  }

  getEmployee(id: string) {
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }

  createEmployee(employee: Employee) {
    return this.http.post(this.apiUrl, employee);
  }

  updateEmployee(id: number, employee: any) {
    return this.http.patch(`${this.apiUrl}/${id}`, employee);
  }

  deleteEmployee(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
