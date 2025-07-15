// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { EmployeeService } from '../../services/employee.service';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';

// @Component({
//   selector: 'app-register',
//   imports: [CommonModule,FormsModule],
//   templateUrl: './register.component.html',
//   styleUrl: './register.component.css'
// })
// export class RegisterComponent {
//  employee = {
//   employeeId:0,
//     name: '',
//     email: '',
//     contactNumber: '',
//     gender:'',
//     address:'',
//     role:'Employee',
//     password: '',
//     confirmPassword: '',
// passwordMismatch: false
//   };

//   successMessage = '';
//   errorMessage = '';

//   constructor(private employeeService: EmployeeService, private router: Router) {}

//   register() {
//     this.employeeService.registerEmployee(this.employee).subscribe({
//       next: () => {
//         this.successMessage = 'Registration successful!';
//         this.errorMessage = '';
//         setTimeout(() => this.router.navigate(['/login']), 2000);
//       },
//       error: (err) => {
//         this.successMessage = '';
//         this.errorMessage = err.error?.message || 'Registration failed.';
//       },
//     });
//   }
// }

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'] // ✅ fixed typo: "styleUrl" → "styleUrls"
})
export class RegisterComponent {
  employee = {
    employeeId: 0,
    name: '',
    email: '',
    password: '',
    gender: '',
    contactNumber: '',
    address: '',
    role: 'Employee'
  };

  confirmPassword: string = '';
  passwordMismatch: boolean = false;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private employeeService: EmployeeService, private router: Router) {}

  cancel() {
  this.router.navigate(['/login']);
}

  register() {
    // check if passwords match
    if (this.employee.password !== this.confirmPassword) {
      this.passwordMismatch = true;
      this.errorMessage = '';
      this.successMessage = '';
      return;
    }

    this.passwordMismatch = false;

    this.employeeService.registerEmployee(this.employee).subscribe({
      next: () => {
        this.successMessage = 'Registration successful!';
        this.errorMessage = '';
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.successMessage = '';
        this.errorMessage = err.error?.message || 'Registration failed.';
      },
    });
  }
}
