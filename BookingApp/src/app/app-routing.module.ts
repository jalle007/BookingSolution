import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {BookingListComponent} from './booking-list/booking-list.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'booking-list' },
  { path: 'booking-list', component: BookingListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
