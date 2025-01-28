import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { HttpService } from '../services/http.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private httpService: HttpService, private router: Router) {}

  canActivate(): boolean | UrlTree {
    if (this.httpService.isLoggedIn()) {
      return true; // Permite el acceso
    } else {
      return this.router.parseUrl('/login'); // Redirige al login si no está autenticado
    }
  }
}
