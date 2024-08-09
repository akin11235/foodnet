using System;

namespace Core.Entities;

public class Meal : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price  { get; set; }
    public required string PictureUrl { get; set; }
    public required string Calories  { get; set; }
    public required string Supplier { get; set; }
    public int  QuantityInStock { get; set; }

}
