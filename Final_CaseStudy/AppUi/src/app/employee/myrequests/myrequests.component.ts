import { Component, OnInit } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { AuthService } from '../../services/auth.service';
import { AssetRequest } from '../../models/assetrequest.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-myrequests',
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './myrequests.component.html',
  styleUrl: './myrequests.component.css'
})
export class MyRequestsComponent implements OnInit {
  myRequests: AssetRequest[] = [];
  showForm = false;
  newRequest = {
    assetId: 0
  };

  constructor(
    private requestService: RequestService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests() {
    const empId = this.authService.getEmployeeId();
    this.requestService.getMyRequests().subscribe({
      next: (data) => (this.myRequests = data),
      error: () => alert('Failed to load requests')
    });
  }

  submitRequest() {
    const empId = this.authService.getEmployeeId();
    const request = {
      employeeId: empId,
      assetId: this.newRequest.assetId,
      requestDate: new Date(),
      status: 'Pending'
    };

    this.requestService.createRequest(request).subscribe({
      next: () => {
        alert('Request submitted successfully!');
        this.showForm = false;
        this.newRequest.assetId = 0;
        this.loadRequests();
      },
      error: () => alert('Error submitting request')
    });
  }
}
