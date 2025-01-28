import { Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { AuthGuard } from '../../guards/auth.guard';

export const dischargeRoutes: Routes = [
  {
    path: 'discharge',
    component: IndexComponent,
    canActivate: [AuthGuard], 
  },
];
