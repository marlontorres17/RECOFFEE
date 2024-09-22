import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter, Routes } from '@angular/router';
import { AppComponent } from './app/app.component';
import { LoginComponent } from './app/pages/login/login.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DasboardComponent } from './app/pages/dasboard/dasboard.component';

// Definir las rutas
const routes: Routes = [
  // Ruta de login sin el layout (sin menú)
  { path: 'login', component: LoginComponent },

  // Ruta principal del dashboard que carga el módulo de dashboard
  {
    path: 'coffe',
    component: DasboardComponent,
    loadChildren: () => import('./app/pages/dasboard/dasboard.module').then(m => m.DashboardModule)
  },

  // Redirección por defecto y rutas no encontradas
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

// Configuración de la aplicación
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync()
  ]
});
