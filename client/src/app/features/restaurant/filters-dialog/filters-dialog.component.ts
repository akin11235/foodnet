import { Component, inject } from '@angular/core';
import { RestaurantService } from '../../../core/services/restaurant.service';
import { MatDivider } from '@angular/material/divider';
import { MatSelectionList } from '@angular/material/list';
import { MatListOption } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filters-dialog',
  standalone: true,
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatButton,
    FormsModule,
  ],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss',
})
export class FiltersDialogComponent {
  restaurantService = inject(RestaurantService);
  private dialogRef = inject(MatDialogRef<FiltersDialogComponent>);
  data = inject(MAT_DIALOG_DATA);

  selectedMealTimes: string[] = this.data.selectedMealTimes;
  selectedMealTypes: string[] = this.data.selectedMealTypes;

  applyFilters() {
    this.dialogRef.close({
      selectedMealTimes: this.selectedMealTimes,
      selectedMealTypes: this.selectedMealTypes,
    });
  }
}
