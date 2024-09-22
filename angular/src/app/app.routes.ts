import { Routes } from '@angular/router';
import path from 'path';
import { HomeComponent } from './pages/home/home.component';

import { MenuComponent } from './pages/menu/menu.component';

import { ModuleComponent } from './pages/module/module.component';
import { PersonComponent } from './pages/person/person.component';
import { ViewComponent } from './pages/view/view.component';
import { UsleComponent } from './pages/usle/usle.component';
import { UserComponent } from './pages/user/user.component';
import { RoviComponent } from './pages/rovi/rovi.component';
import { LoginComponent } from './pages/login/login.component';
import { AppComponent } from './app.component';
import { CountryComponent } from './pages/country/country.component';
import { DepartmentComponent } from './pages/department/department.component';
import { CityComponent } from './pages/city/city.component';
import { RoleComponent } from './pages/role/role.component';
import { FarmComponent } from './pages/farm/farm.component';
import { BenefitComponent } from './pages/benefit/benefit.component';
import { HarvestComponent } from './pages/harvest/harvest.component';
import { CollectionDetailComponent } from './pages/collection-detail/collection-detail.component';
import { PersonBenefitComponent } from './pages/person-benefit/person-benefit.component';
import { LiquidationComponent } from './pages/liquidation/liquidation.component';
import { LotComponent } from './pages/lot/lot.component';

export const routes: Routes = [
    {
        path: '',
        component: AppComponent, // El layout con el menú
        children: [
          { path: 'dasboard/home', component: HomeComponent },
          { path: 'dasboard/role', component: RoleComponent },
          { path: 'dasboard/module', component: ModuleComponent },
          { path: 'dasboard/person', component: PersonComponent },
          { path: 'dasboard/view', component: ViewComponent },
          { path: 'dasboard/user', component: UserComponent },
          { path: 'dasboard/usle', component: UsleComponent },
          { path: 'dasboard/rovi', component: RoviComponent },
          { path: 'dasboard/country', component: CountryComponent },
          { path: 'dasboard/department', component: DepartmentComponent },
          { path: 'dasboard/city', component: CityComponent },

          { path: 'dasboard/farm', component: FarmComponent },
          { path: 'dasboard/benefit', component: BenefitComponent },
          { path: 'dasboard/harvest', component: HarvestComponent },
          { path: 'dasboard/collection-detail', component: CollectionDetailComponent },
          { path: 'dasboard/person-benefit', component: PersonBenefitComponent },
          { path: 'dasboard/liquidation', component: LiquidationComponent },
          { path: 'dasboard/lot', component: LotComponent },




        ],
      },
    

    
 
];
