import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { RestaurantComponent } from './features/restaurant/restaurant.component';
import { MenuitemDetailsComponent } from './features/restaurant/menuitem-details/menuitem-details.component';
import { CartComponent } from './features/cart/cart.component';
import { CheckoutComponent } from './features/checkout/checkout.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'restaurant', component: RestaurantComponent },
  { path: 'restaurant/:id', component: MenuitemDetailsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
