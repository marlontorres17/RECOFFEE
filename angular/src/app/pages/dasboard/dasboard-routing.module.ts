import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DasboardComponent } from './dasboard.component';
import { HomeComponent } from '../home/home.component';
import { ModuleComponent } from '../module/module.component';
import { PersonComponent } from '../person/person.component';
import { RoleComponent } from '../role/role.component';
import { RoviComponent } from '../rovi/rovi.component';
import { UserComponent } from '../user/user.component';
import { UsleComponent } from '../usle/usle.component';
import { ViewComponent } from '../view/view.component';
import { AuthGuard } from '../login/auth.guard';
import { CountryComponent } from '../country/country.component';
import { DepartmentComponent } from '../department/department.component';
import { CityComponent } from '../city/city.component';

const routes: Routes = [
  {
    path: '',
    component: DasboardComponent,
    children: [
      { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/role', component: RoleComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/module', component: ModuleComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/person', component: PersonComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/view', component: ViewComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/user', component: UserComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/usle', component: UsleComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/rovi', component: RoviComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/country', component: CountryComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/department', component: DepartmentComponent, canActivate: [AuthGuard] },
      { path: 'seguridad/city', component: CityComponent, canActivate: [AuthGuard] },


      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: 'login', loadComponent: () => import('../login/login.component').then(m => m.LoginComponent) }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
