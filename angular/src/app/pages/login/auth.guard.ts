import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  private isLogin: boolean;

  constructor(private router: Router) {
    const storedLoginStatus = localStorage.getItem('isLogin');
    this.isLogin = storedLoginStatus === 'true'; // Leer estado desde localStorage
  }

  canActivate(): boolean {
    if (this.isLogin) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }

  setLoginStatus(status: boolean) {
    this.isLogin = status;
    localStorage.setItem('isLogin', String(status)); // Actualizar localStorage cuando cambie el estado
  }
}
