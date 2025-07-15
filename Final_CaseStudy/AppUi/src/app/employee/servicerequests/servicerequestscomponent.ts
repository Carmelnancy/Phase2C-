import { Component, OnInit } from '@angular/core';
import { ServiceRequestService } from '../../services/servicerequest.service';
import { AuthService } from '../../services/auth.service';
import { ServiceRequest } from '../../models/servicerequest.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-servicerequests',
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './servicerequests.component.html',
  styleUrl: './servicerequests.component.css'
})
export class ServiceRequestsComponent implements OnInit {
  showForm = false;
  serviceRequests: ServiceRequest[] = [];
  newRequest = {
    assetId: 0,
      description: '',
      issueType: ''
  };

  constructor(
    private serviceRequestService: ServiceRequestService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const employeeId = this.authService.getEmployeeId();
    this.serviceRequestService.getRequestsByEmployee().subscribe({
      next: (data) => (this.serviceRequests = data),
      error: (err) => alert('Error fetching service requests')
    });
  }

  submitServiceRequest() {
    const employeeId = this.authService.getEmployeeId();
    const request = {
      employeeId: employeeId,
      assetId: this.newRequest.assetId,
      description: this.newRequest.description,
      issueType: this.newRequest.issueType,
      requestDate: new Date(),
      status: 'Pending'
    };

    this.serviceRequestService.createServiceRequest(request).subscribe({
      next: () => {
        alert('Service Request Submitted!');
        this.showForm = false;
        this.ngOnInit(); // Refresh list
      },
      error: () => alert('Error submitting service request')
    });
  }
}
