import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthGuard } from '../pages/login/auth.guard';

@Injectable({
  providedIn: 'root'  // Esto permite que el servicio esté disponible en toda la aplicación
})
export class AuthService {

  constructor(private router: Router, private authGuard: AuthGuard) {}

  // Método para cerrar sesión
  logout() {
    // Eliminar el estado de autenticación del localStorage
    localStorage.removeItem('isLogin');
    // Actualizar el estado en el guard
    this.authGuard.setLoginStatus(false);
    // Redirigir al usuario a la página de login
    this.router.navigate(['/login']);
  }
}
