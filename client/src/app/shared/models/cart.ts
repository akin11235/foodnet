import { nanoid } from 'nanoid';

export type CartType = {
  id: string;
  items: CartItem[];
};

export type CartItem = {
  menuItemId: number;
  menuItemName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  mealTime: string;
  mealType: string;
};

export class Cart implements CartType {
  id = nanoid();
  items: CartItem[] = [];
}
