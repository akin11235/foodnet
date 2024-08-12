using System;

namespace Core.Entities;

public class MenuItem : BaseEntity // MenuItem
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price  { get; set; }
    public required string PictureUrl { get; set; }
    public required string MealType  { get; set; } // cuisine: Indian, Mediterranean
    public required string MealTime { get; set; } // Mealtime
    public int  QuantityInStock { get; set; }

    // Beverage

}
