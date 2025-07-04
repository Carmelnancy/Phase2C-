import { Component } from '@angular/core';
import { Employee } from './models/employee.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee',
  imports: [CommonModule],
  templateUrl: './employee.html',
  styleUrl: './employee.css'
})
export class EmployeeComponent {

  employees: Employee[]=[
    {name:"Alice",department:"IT",isPresent:true,workFromHome:false},
    {name:"Bob",department:"HR",isPresent:false,workFromHome:true},
    {name:"Charlie",department:"Sales",isPresent:false,workFromHome:false},
  ]
}
