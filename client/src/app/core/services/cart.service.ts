import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/cart';
import { MenuItem } from '../../shared/models/menuitems';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  cart = signal<Cart | null>(null);
  itemCount = computed(() => {
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0);
  });

  totals = computed(() => {
    const cart = this.cart();

    if (!cart) return null;
    const subtotal = cart.items.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    );

    const shipping = 0;
    const discount = 0;

    return {
      subtotal,
      shipping,
      discount,
      total: subtotal + shipping - discount,
    };
  });

  getCart(id: string) {
    return this.http.get<Cart>(this.baseUrl + 'cart?id=' + id).pipe(
      map((cart) => {
        this.cart.set(cart);
        return cart;
      })
    );
  }

  setCart(cart: Cart) {
    return this.http.post<Cart>(this.baseUrl + 'cart', cart).subscribe({
      next: (cart) => this.cart.set(cart),
    });
  }

  addItemToCart(item: CartItem | MenuItem, quantity = 1) {
    const cart = this.cart() ?? this.createCart();
    if (this.isMenuItem(item)) {
      item = this.mapMenuItemToCartItem(item);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, quantity);
    this.setCart(cart);
  }

  removeItemFromCart(menuItemId: number, quantity = 1) {
    const cart = this.cart();
    if (!cart) return;
    const index = cart.items.findIndex((x) => x.menuItemId === menuItemId);
    if (index !== -1) {
      if (cart.items[index].quantity > quantity) {
        cart.items[index].quantity -= quantity;
      } else {
        cart.items.splice(index, 1);
      }
      if (cart.items.length === 0) {
        this.deleteCart();
      } else {
        this.setCart(cart);
      }
    }
  }

  deleteCart() {
    this.http.delete(this.baseUrl + 'cart?id=' + this.cart()?.id).subscribe({
      next: () => {
        localStorage.removeItem('cart_id');
        this.cart.set(null);
      },
    });
  }

  private addOrUpdateItem(
    items: CartItem[],
    item: CartItem,
    quantity: number
  ): CartItem[] {
    const index = items.findIndex((x) => x.menuItemId === item.menuItemId);
    if (index === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      items[index].quantity += quantity;
    }

    return items;
  }

  private mapMenuItemToCartItem(item: MenuItem): CartItem {
    return {
      menuItemId: item.id,
      menuItemName: item.name,
      price: item.price,
      quantity: 0,
      pictureUrl: item.pictureUrl,
      mealTime: item.mealTime,
      mealType: item.mealType,
    };
  }

  private isMenuItem(item: CartItem | MenuItem): item is MenuItem {
    return (item as MenuItem).id !== undefined;
  }

  private createCart(): Cart {
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }
}
