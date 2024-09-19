import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthGuard } from '../login/auth.guard';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule]
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private authGuard: AuthGuard
  ) {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });

    // Verificar si ya hay una sesión iniciada al cargar el componente
    const storedLoginStatus = localStorage.getItem('isLogin');
    if (storedLoginStatus === 'true') {
      this.authGuard.setLoginStatus(true);
      this.router.navigate(['dashboard/home']);
    }
  }

  onSubmit(event: Event) {
    event.preventDefault();  // Evitar que la página se recargue

    if (this.loginForm.valid) {  // Validar el formulario antes de autenticar
      const loginData = this.loginForm.value;
      this.http.post('http://localhost:9191/api/Login/login', loginData)
        .subscribe(
          (response) => {
            this.authGuard.setLoginStatus(true);
            localStorage.setItem('isLogin', 'true'); // Guardar estado en localStorage
            this.router.navigate(['coffe/home']);
          },
          (error) => {
            this.errorMessage = 'Usuario o contraseña incorrectos.';
            this.authGuard.setLoginStatus(false);
            localStorage.setItem('isLogin', 'false'); // Guardar estado en localStorage
          }
        );
    } else {
      this.errorMessage = 'Por favor, complete todos los campos requeridos.';
    }
  }
}
