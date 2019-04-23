import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import {UserService} from "./services/user.service";
import {VeloService} from "./services/velo.service";
import {NgxSoapModule} from "ngx-soap";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    NgxSoapModule,
    AppRoutingModule
  ],
  providers: [
    UserService,
    VeloService],
  bootstrap: [AppComponent]
})
export class AppModule { }
