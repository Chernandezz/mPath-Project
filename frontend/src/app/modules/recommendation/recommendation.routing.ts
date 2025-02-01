import { Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { AuthGuard } from '../../guards/auth.guard';

export const recommendationRoutes: Routes = [
  {
    path: 'recommendation',
    component: IndexComponent,
    canActivate: [AuthGuard],
  },
];
