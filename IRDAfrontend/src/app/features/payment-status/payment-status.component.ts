import { Component, OnDestroy } from '@angular/core';
import { GetClaimStatus } from '../models/claimstatus.model';
import { Subscription } from 'rxjs';
import { ClaimstatusService } from '../services/claimstatus.service';
import { PaymentReport } from '../models/paymentreport.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-status',
  templateUrl: './payment-status.component.html',
  styleUrl: './payment-status.component.css'
})
export class PaymentStatusComponent{

  model: GetClaimStatus;
  private PaymentStatusSubscription?: Subscription;
  private UpdatePaymentSubscription?: Subscription;
  paymentreport: PaymentReport;
  errorMessage= {message:''};
  updatevar:number=0;

  constructor(private router:Router, private service: ClaimstatusService) {
    this.model = {
      month: '',
      year: null
    };
  }

  OnFormSubmit(){
    this.PaymentStatusSubscription = this.service.getpaymentreport(this.model)
    .subscribe({
      next: (response) => {
        this.paymentreport = response[0];
        console.log(this.paymentreport);
        this.errorMessage.message = '';
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error:', error);
        this.errorMessage.message = error.error.message;
        this.updatevar=0;
        // alert(this.errorMessageSub.message);
        // this.router.navigateByUrl('/claimstatus/report');
      }
    });
  }

  UpdatePayment(){
    this.UpdatePaymentSubscription = this.service.postpaymentstatus(this.model)
    .subscribe({
      next: (response) => {
        console.log(response);
        this.errorMessage.message = "Database Updated Successfully!";
        this.updatevar = 1;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error:', error);
        this.errorMessage.message = error.error.message;
        this.updatevar = 0;
      }
    });
  }
}
