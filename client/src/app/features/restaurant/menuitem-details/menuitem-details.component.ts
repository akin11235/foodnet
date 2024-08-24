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
  ],
  templateUrl: './menuitem-details.component.html',
  styleUrl: './menuitem-details.component.scss',
})
export class MenuitemDetailsComponent implements OnInit {
  private retaurantService = inject(RestaurantService);
  private activatedRoute = inject(ActivatedRoute);
  menuitem?: MenuItem;

  ngOnInit(): void {
    this.loadMenuItem();
  }

  loadMenuItem() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.retaurantService.getMenuitem(+id).subscribe({
      next: (menuitem) => (this.menuitem = menuitem),
      error: (error) => console.log(error),
    });
  }
}
