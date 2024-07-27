import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetClaimStatus } from '../models/claimstatus.model';
import { Observable } from 'rxjs';
import { ClaimReport } from '../models/claimreport.model';
import { PaymentReport } from '../models/paymentreport.model';

@Injectable({
  providedIn: 'root'
})
export class ClaimstatusService {

  constructor(private http:HttpClient) { }

  getclaimreport(model: GetClaimStatus): Observable<ClaimReport[]>{
    return this.http.get<ClaimReport[]>('http://localhost:5107/irda/claimStatus/report/'+model.month+'/'+model.year)
  }

  getpaymentreport(model: GetClaimStatus): Observable<PaymentReport>{
    return this.http.get<PaymentReport>('http://localhost:5107/irda/paymentStatus/report/'+model.month+'/'+model.year)
  }

  postclaimstatus(model: GetClaimStatus): Observable<any>{
    return this.http.post<any>('http://localhost:5107/irda/claimStatus/pull/'+model.month+'/'+model.year, model)
  }

  postpaymentstatus(model: GetClaimStatus): Observable<any>{
    return this.http.post<any>('http://localhost:5107/irda/paymentStatus/pull/'+model.month+'/'+model.year, model)
  }
}
