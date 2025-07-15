export interface AuditRequest {
  auditRequestId: number;
  employeeId: number;
  assetId: number;
  requestDate: Date;
  status: string;
}
