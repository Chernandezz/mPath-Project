import { Routes } from '@angular/router';
import { HeaderComponent } from './modules/global/components/header/header.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: HeaderComponent,
    loadChildren: () => import('./routes.module').then((m) => m.RoutesModule),
  },
  {
    path: '**',
    component: HeaderComponent,
  },
];
