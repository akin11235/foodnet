import { Component, inject, OnInit } from '@angular/core';
import { MenuItem } from '../../shared/models/menuitems';
import { RestaurantService } from '../../core/services/restaurant.service';
import { MatCard } from '@angular/material/card';
import { MenuitemItemComponent } from './menuitem-item/menuitem-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import {
  MatListOption,
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { RestaurantParams } from '../../shared/models/restaurantParams';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-restaurant',
  standalone: true,
  imports: [
    MatCard,
    MenuitemItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule,
  ],
  templateUrl: './restaurant.component.html',
  styleUrl: './restaurant.component.scss',
})
export class RestaurantComponent implements OnInit {
  private restaurantService = inject(RestaurantService);
  private dialogService = inject(MatDialog);
  menuitems?: Pagination<MenuItem>;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low-High', value: 'priceAsc' },
    { name: 'Price: High-Low', value: 'priceDesc' },
  ];

  restaurantParams = new RestaurantParams();
  pageSizeOptions = [5, 10, 15, 20];

  heroImageUrl: string = '';

  ngOnInit(): void {
    this.initializeRestaurant();
  }

  initializeRestaurant() {
    this.restaurantService.getMealTimes();
    this.restaurantService.getMealTypes();
    this.getMenuItems();
  }

  getMenuItems() {
    this.restaurantService.getMenuItems(this.restaurantParams).subscribe({
      next: (response) => {
        this.menuitems = response;

        if (this.restaurantParams.pageSize > 0) {
          this.heroImageUrl = this.menuitems.data[0].pictureUrl;
        }
      },

      error: (error) => console.log(error),
    });
  }

  onSearchChange() {
    this.restaurantParams.pageNumber = 1;
    this.getMenuItems();
  }

  handlePageEvent(event: PageEvent) {
    this.restaurantParams.pageNumber = event.pageIndex + 1;
    this.restaurantParams.pageSize = event.pageSize;
    this.getMenuItems();
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.restaurantParams.sort = selectedOption.value;
      this.restaurantParams.pageNumber = 1;
      this.getMenuItems();
    }
  }

  openFiltersDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedMealTimes: this.restaurantParams.mealTimes,
        selectedMealTypes: this.restaurantParams.mealTypes,
      },
    });
    dialogRef.afterClosed().subscribe({
      next: (result) => {
        if (result) {
          this.restaurantParams.mealTimes = result.selectedMealTimes;
          this.restaurantParams.mealTypes = result.selectedMealTypes;
          this.restaurantParams.pageNumber = 1;
          this.getMenuItems();
        }
      },
    });
  }
}
