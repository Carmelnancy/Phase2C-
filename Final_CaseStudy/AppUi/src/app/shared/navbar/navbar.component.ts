import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule,RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

    constructor(private router: Router) {}

  get employeeId(): number {
    return Number(localStorage.getItem('employeeId'));
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
