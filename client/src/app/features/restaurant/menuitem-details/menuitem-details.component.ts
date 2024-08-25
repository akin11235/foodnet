import { Component, inject, OnInit } from '@angular/core';
import { RestaurantService } from '../../../core/services/restaurant.service';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from '../../../shared/models/menuitems';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartService } from '../../../core/services/cart.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-menuitem-details',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider,
    FormsModule,
  ],
  templateUrl: './menuitem-details.component.html',
  styleUrl: './menuitem-details.component.scss',
})
export class MenuitemDetailsComponent implements OnInit {
  private retaurantService = inject(RestaurantService);
  private activatedRoute = inject(ActivatedRoute);
  private cartService = inject(CartService);
  menuitem?: MenuItem;
  quantityInCart = 0;
  quantity = 1;

  ngOnInit(): void {
    this.loadMenuItem();
  }

  loadMenuItem() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.retaurantService.getMenuitem(+id).subscribe({
      next: (menuitem) => {
        this.menuitem = menuitem;
        this.updateQuantityInCart();
      },
      error: (error) => console.log(error),
    });
  }

  updateCart() {
    if (!this.menuitem) return;
    if (this.quantity > this.quantityInCart) {
      const itemsToAdd = this.quantity - this.quantityInCart;
      this.quantityInCart += itemsToAdd;
      this.cartService.addItemToCart(this.menuitem, itemsToAdd);
    } else {
      const itemsToRemove = this.quantityInCart - this.quantity;
      this.quantityInCart -= itemsToRemove;
      this.cartService.removeItemFromCart(this.menuitem.id, itemsToRemove);
    }
  }

  updateQuantityInCart() {
    this.quantityInCart =
      this.cartService
        .cart()
        ?.items.find((x) => x.menuItemId === this.menuitem?.id)?.quantity || 0;
    this.quantity = this.quantityInCart || 1;
  }

  getButtonText() {
    return this.quantityInCart > 0 ? 'Update cart' : 'Add to cart';
  }
}
