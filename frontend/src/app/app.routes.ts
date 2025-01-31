import { Routes, provideRouter } from '@angular/router';
import { IndexComponent } from './modules/global/components/index/index.component';
import { LoginComponent } from './modules/global/components/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { doctorRoutes } from './modules/doctor/doctor.routing';
import { admissionRoutes } from './modules/admission/admission.routing';
import { patientRoutes } from './modules/patient/patient.routing';
import { dischargeRoutes } from './modules/discharge/discharge.routing';
import { HeaderComponent } from './modules/global/components/header/header.component';

export const routes: Routes = [
  { path: '', component: IndexComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  ...doctorRoutes,
  ...admissionRoutes,
  ...patientRoutes,
  ...dischargeRoutes,
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

export const appConfig = {
  providers: [provideRouter(routes)],
};
