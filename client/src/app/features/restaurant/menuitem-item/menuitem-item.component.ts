import { Component, Input } from '@angular/core';
import { MenuItem } from '../../../shared/models/menuitems';
import {
  MatCard,
  MatCardActions,
  MatCardContent,
} from '@angular/material/card';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-menuitem-item',
  standalone: true,
  imports: [
    MatCard,
    MatCardContent,
    CurrencyPipe,
    MatCardActions,
    MatButton,
    MatIcon,
  ],
  templateUrl: './menuitem-item.component.html',
  styleUrl: './menuitem-item.component.scss',
})
export class MenuitemItemComponent {
  @Input() menuitem?: MenuItem;
}
