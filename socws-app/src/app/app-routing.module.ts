import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {LoginComponent} from './components/login/login.component';
import {RegisterComponent} from "./components/register/register.component";
import {VelibComponent} from "./components/velib-components/velib/velib.component";
import {VelibCitiesComponent} from "./components/velib-components/velib-cities/velib-cities.component";
import {VelibStationComponent} from "./components/velib-components/velib-station/velib-station.component";
import {UpgradeComponent} from "./components/upgrade/upgrade.component";

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path :'', component: VelibComponent},
  {path:'cities/:city', component: VelibCitiesComponent},
  {path:'stations/:city/:station', component: VelibStationComponent},
  {path:'upgrade', component: UpgradeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
