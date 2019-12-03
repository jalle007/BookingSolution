import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Booking } from '../shared/booking';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RestApiService {

  apiURL = 'http://localhost:50991/api'; //api ?

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }  

    // HttpClient API get() method => Fetch bookings
    getBookings(): Observable<Booking> {
      return this.http.get<Booking>(this.apiURL + '/booking')
      .pipe(
        retry(1),
        catchError(this.handleError)
      )
    }  

       //Seed data
       GetSeed(): Observable<Booking> {
        return this.http.get<Booking>(this.apiURL + '/booking/GetSeed')
        .pipe(
          retry(1),
          catchError(this.handleError)
        )
      }  
    
     // HttpClient API delete() method => Delete Booking
  deleteBooking(id){
    return this.http.delete<Booking>(this.apiURL + '/booking/' + id, this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

      // Error handling 
  handleError(error) {
    console.log(error);
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
 }

}
