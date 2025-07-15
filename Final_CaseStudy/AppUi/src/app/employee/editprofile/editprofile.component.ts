import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-editprofile',
  imports: [CommonModule,FormsModule],
  templateUrl: './editprofile.component.html',
  styleUrl: './editprofile.component.css'
})
export class EditProfileComponent implements OnInit {
  employee: any = {};
  successMessage = '';
  errorMessage = '';

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private location: Location
  ) {}

goBack() {
  this.location.back();
}

  ngOnInit(): void {
    const empId = localStorage.getItem('employeeId');
    if (empId) {
      this.employeeService.getEmployeeById(parseInt(empId)).subscribe({
        next: (emp) => this.employee = emp,
        error: () => this.errorMessage = 'Unable to load employee data'
      });
    }
  }

  // updateProfile() {
  //   this.employeeService.updateEmployee(this.employee).subscribe({
  //     next: () => {
  //       this.successMessage = 'Profile updated successfully!';
  //       this.errorMessage = '';
  //       setTimeout(() => this.router.navigate(['/dashboard']), 2000);
  //     },
  //     error: () => this.errorMessage = 'Update failed. Try again.'
  //   });
  // }
  updateProfile() {
  this.employeeService.updateEmployee(this.employee).subscribe({
    next: () => {
      alert('Profile updated. Please log in again.');
      localStorage.removeItem('token');
      localStorage.removeItem('employeeId');
      this.router.navigate(['/login']);
    },
    error: () => {
      this.errorMessage = 'Failed to update profile.';
    }
  });
}


}
