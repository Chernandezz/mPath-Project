import { Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { AuthGuard } from '../../guards/auth.guard';

export const patientRoutes: Routes = [
  {
    path: 'patient',
    component: IndexComponent,
    canActivate: [AuthGuard],
  },
];
