
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { ButtonsModule,  ModalModule, TooltipModule,  BsDropdownModule, BsDatepickerModule } from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
 
import { ToastrModule } from 'ngx-toastr';

import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';

import { EventoService } from './_services/evento.service';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { PalestranteComponent } from './palestrante/palestrante.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContatoComponent } from './contato/contato.component';
import { TituloComponent } from './_shared/titulo/titulo.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { AuthInterceptor } from './auth/auth.interceptor';




@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe,
      PalestranteComponent,
      DashboardComponent,
      ContatoComponent,
      TituloComponent,
      UserComponent,
      LoginComponent,
      RegistrationComponent
     
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      ModalModule.forRoot(),
      ButtonsModule.forRoot(),
      TooltipModule.forRoot(),
      BsDatepickerModule.forRoot(),
      BrowserAnimationsModule,
      ToastrModule.forRoot(),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule
   ],
   providers: [
      EventoService,
      {
         provide: HTTP_INTERCEPTORS,
         useClass:AuthInterceptor,
         multi: true
      }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
