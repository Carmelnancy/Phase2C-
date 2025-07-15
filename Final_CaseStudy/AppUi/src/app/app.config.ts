import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter,Routes } from '@angular/router';
import { provideHttpClient ,withFetch} from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { DashboardComponent } from './employee/dashboard/dashboard.component';
import { MyRequestsComponent } from './employee/myrequests/myrequests.component';
import { ServiceRequestsComponent } from './employee/servicerequests/servicerequestscomponent';
import { AuditHistoryComponent } from './employee/audithistory/audithistory.component';
import { AuthGuard } from './auth/auth-guard';
import { EditProfileComponent } from './employee/editprofile/editprofile.component';

const myRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'editprofile', component: EditProfileComponent, canActivate: [AuthGuard] },
  { path: 'myrequests', component: MyRequestsComponent, canActivate: [AuthGuard] },
  { path: 'servicerequests', component: ServiceRequestsComponent, canActivate: [AuthGuard] },
  { path: 'audithistory', component: AuditHistoryComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];
export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    importProvidersFrom(FormsModule),
    provideHttpClient(withFetch()),
    provideRouter(myRoutes)
  ]
};
