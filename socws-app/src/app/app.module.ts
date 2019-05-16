import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import {UserService} from './services/user.service';
import {VeloService} from './services/velo.service';
import {NgxSoapModule} from 'ngx-soap';
import {FormsModule} from '@angular/forms';
import {AngularFireStorageModule} from 'angularfire2/storage';
import {AngularFirestoreModule} from 'angularfire2/firestore';
import {AngularFireAuth, AngularFireAuthModule} from 'angularfire2/auth';
import {AngularFireModule} from 'angularfire2';
import {environment} from '../environments/environment';
import { RegisterComponent } from './components/register/register.component';
import { VelibComponent } from './components/velib-components/velib/velib.component';
import { VelibCitiesComponent } from './components/velib-components/velib-cities/velib-cities.component';
import { VelibStationComponent } from './components/velib-components/velib-station/velib-station.component';
import { UpgradeComponent } from './components/upgrade/upgrade.component';
import { MonitoringComponent } from './components/monitoring/monitoring.component';
import {MonitoringService} from "./services/monitoring.service";


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    VelibComponent,
    VelibCitiesComponent,
    VelibStationComponent,
    UpgradeComponent,
    MonitoringComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    NgxSoapModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule,
    AngularFireStorageModule,
    AngularFireAuthModule
  ],
  providers: [
    UserService,
    AngularFireAuth,
    VeloService,
    MonitoringService],
  bootstrap: [AppComponent]
})
export class AppModule { }
