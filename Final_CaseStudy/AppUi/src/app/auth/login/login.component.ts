import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData = {
    email: '',
    password: '',
    role:'Employee'
  };

  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {
    console.log('LoginComponent loaded');

  }

  onSubmit() {
     console.log('Login data:', this.loginData); 
    this.authService.login(this.loginData).subscribe({
      next: (res) => {
        console.log('Login success:', res);
        this.authService.storeSessionDetails(res);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error('Login error:', err); 
        this.errorMessage = 'Invalid email or password';
      }
    });
  }
}
