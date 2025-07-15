import { Component, OnInit } from '@angular/core';
import { AssetService } from '../../services/asset.service';
import { EmployeeService } from '../../services/employee.service';
import { Asset } from '../../models/asset.model';
import { Employee } from '../../models/employee.model';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  employee!: Employee;
  allAssets: Asset[] = [];
  myAssets: any[] = [];

  constructor(
    private assetService: AssetService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEmployeeProfile();
    this.loadAllAssets();
    this.loadMyAssets();
  }

  loadEmployeeProfile() {
    this.employeeService.getProfile().subscribe({
      next: (data) => (this.employee = data),
      error: () => alert('Failed to load employee profile')
    });
  }

  loadAllAssets() {
    this.assetService.getAllAssets().subscribe({
      next: (data) => (this.allAssets = data),
      error: () => alert('Failed to load assets')
    });
  }
//   loadMyAssets() {
//   this.employeeService.getMyAssets().subscribe({
//     next: (data) => {
//       if (data && data.length > 0) {
//         this.myAssets = data;
//       } else {
//         this.myAssets = [];
//         console.log("No assets assigned to you.");
//       }
//     },
//     error: () => alert('Something went wrong while fetching assets') // real error only
//   });
// }

loadMyAssets() {
  this.employeeService.getMyAssets().subscribe({
    next: (data) => {
      this.myAssets = data;
    },
    error: (error) => {
      if (error.status === 404) {
        // No assets assigned yet – just keep array empty
        this.myAssets = [];
      } else {
        alert('Something went wrong while fetching assets');
      }
    }
  });
}

editProfile() {
  this.router.navigate(['/editprofile']);
}


  // loadMyAssets() {
  //   this.employeeService.getMyAssets().subscribe({
  //     next: (data) => (this.myAssets = data),
  //     error: () => alert('Failed to load assigned assets')
  //   });
  // }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
