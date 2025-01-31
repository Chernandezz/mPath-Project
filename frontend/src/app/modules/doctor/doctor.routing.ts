import { Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { AuthGuard } from '../../guards/auth.guard';

export const doctorRoutes: Routes = [
  {
    path: 'doctor',
    component: IndexComponent,
    canActivate: [AuthGuard],
  },
];
