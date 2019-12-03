import { Component, OnInit } from '@angular/core';
import { RestApiService } from "../shared/rest-api.service";

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {

  Bookings: any = [];

  constructor(public restApi: RestApiService) { }

  ngOnInit() {
    this.loadBookings();
  }

   // Get Booking list
   loadBookings() {
    return this.restApi.getBookings().subscribe((data: {}) => {
      this.Bookings = data;
      console.log(data);

    })
  }

    // Delete Booking
    deleteBooking(id) {
      if (window.confirm('Are you sure, you want to delete?')){
        this.restApi.deleteBooking(id).subscribe(data => {
          this.loadBookings()
        })
      }
    } 

// Delete Booking
GetSeed() {
  return this.restApi.GetSeed().subscribe((data: {}) => {
    this.Bookings = data;
    
    this.loadBookings();

  })
}

}


