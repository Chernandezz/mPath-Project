import { Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { AuthGuard } from '../../guards/auth.guard';

export const admissionRoutes: Routes = [
  {
    path: 'admission',
    component: IndexComponent,
    canActivate: [AuthGuard], 
  },
];
