import { Component, OnInit } from '@angular/core';
import { AuditRequestService } from '../../services/auditrequest.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../shared/navbar/navbar.component';
import { AuditRequest } from '../../models/auditrequest.model';

@Component({
  selector: 'app-audithistory',
  imports: [CommonModule, NavbarComponent],
  templateUrl: './audithistory.component.html',
  styleUrl: './audithistory.component.css'
})
export class AuditHistoryComponent implements OnInit {
  audits: AuditRequest[] = [];

  constructor(
    private auditService: AuditRequestService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const empId = this.authService.getEmployeeId();
    this.auditService.getMyAuditRequests().subscribe({
      next: (data) => (this.audits = data),
      error: () => alert('Error fetching audit history')
    });
  }
}
