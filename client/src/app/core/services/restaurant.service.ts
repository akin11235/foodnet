import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { MenuItem } from '../../shared/models/menuitems';
import { RestaurantParams } from '../../shared/models/restaurantParams';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);

  mealTimes: string[] = [];
  mealTypes: string[] = [];

  getMenuItems(restaurantParams: RestaurantParams) {
    let params = new HttpParams();

    if (restaurantParams.mealTimes.length > 0) {
      params = params.append('mealTimes', restaurantParams.mealTimes.join(','));
    }
    if (restaurantParams.mealTypes.length > 0) {
      params = params.append('mealTypes', restaurantParams.mealTypes.join(','));
    }

    if (restaurantParams.sort) {
      params = params.append('sort', restaurantParams.sort);
    }

    if (restaurantParams.search) {
      params = params.append('search', restaurantParams.search);
    }

    params = params.append('pageSize', restaurantParams.pageSize);
    params = params.append('pageIndex', restaurantParams.pageNumber);

    return this.http.get<Pagination<MenuItem>>(this.baseUrl + 'menuitems', {
      params,
    });
  }

  getMealTimes() {
    if (this.mealTimes.length > 0) return;
    return this.http
      .get<string[]>(this.baseUrl + 'menuitems/mealtimes')
      .subscribe({
        next: (response) => (this.mealTimes = response),
      });
  }

  getMealTypes() {
    if (this.mealTypes.length > 0) return;
    return this.http
      .get<string[]>(this.baseUrl + 'menuitems/mealtypes')
      .subscribe({
        next: (response) => (this.mealTypes = response),
      });
  }
}
