import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClaimStatusComponent } from './features/claim-status/claim-status.component';
import { PaymentStatusComponent } from './features/payment-status/payment-status.component';
import { HomeCompComponent } from './features/home-comp/home-comp.component';

const routes: Routes = [
  {
    path: 'claimstatus/report',
    component: ClaimStatusComponent
  },
  {
    path: 'paymentstatus/report',
    component: PaymentStatusComponent
  },
  {
    path:'',
    component: HomeCompComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
