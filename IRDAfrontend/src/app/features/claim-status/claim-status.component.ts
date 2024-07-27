import { Component, OnDestroy } from '@angular/core';
import { GetClaimStatus } from '../models/claimstatus.model';
import { ClaimstatusService } from '../services/claimstatus.service';
import { Subscription } from 'rxjs';
import { ClaimReport } from '../models/claimreport.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-claim-status',
  templateUrl: './claim-status.component.html',
  styleUrl: './claim-status.component.css'
})
export class ClaimStatusComponent{

  model: GetClaimStatus;
  private ClaimStatusSubscription?: Subscription;
  private UpdateClaimSubscription?: Subscription;
  claimstatusmodel: ClaimReport[];
  errorMessage= {message:''};
  updatevar:number=0;

  constructor(private router:Router, private service: ClaimstatusService) {
    this.model = {
      month: '',
      year: null
    };
  }

  OnFormSubmit(){
    this.ClaimStatusSubscription = this.service.getclaimreport(this.model)
    .subscribe({
      next: (response) => {
        this.claimstatusmodel = response;
        console.log(this.claimstatusmodel);
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

  UpdateClaims(){
    this.UpdateClaimSubscription = this.service.postclaimstatus(this.model)
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
