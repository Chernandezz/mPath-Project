import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  Router,
  UrlTree,
} from '@angular/router';
import { AuthService } from '../core/services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean | UrlTree {
    if (!this.authService.isTokenValid()) {
      this.authService.logout();
      return this.router.parseUrl('/login');
    }

    const userRole = this.authService.getUserRole();
    const allowedRoles = route.data['roles'] as string[];

    if (allowedRoles && userRole && !allowedRoles.includes(userRole)) {
      return this.router.parseUrl('/unauthorized');
    }


    return true;
  }
}
