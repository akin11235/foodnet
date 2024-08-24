using System;

namespace Core.Entities;

public class CartItem
{
    public int MenuItemId { get; set; }
    public required string MenuItemName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public required string PictureUrl { get; set; }
    public required string MealTime { get; set; }
    public required string MealType { get; set; }

}
