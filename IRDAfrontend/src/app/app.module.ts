import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { ClaimStatusComponent } from './features/claim-status/claim-status.component';
import { PaymentStatusComponent } from './features/payment-status/payment-status.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeCompComponent } from './features/home-comp/home-comp.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ClaimStatusComponent,
    PaymentStatusComponent,
    HomeCompComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    // provideClientHydration()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
