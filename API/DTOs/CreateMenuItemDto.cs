using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class CreateMenuItemDto
{

    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; }= string.Empty;
    [Range(0.01, double.MaxValue, ErrorMessage ="Price must be greater than 0")]
    public decimal Price  { get; set; }
    [Required]
    public string PictureUrl { get; set; }= string.Empty;
    [Required]    
    public string MealType  { get; set; } = string.Empty;// cuisine: Indian, Mediterranean
    [Required]
    public string MealTime { get; set; }  = string.Empty;// Mealtime
    [Range(1, int.MaxValue, ErrorMessage ="Quantity in stock must be at least 1")]

    public int  QuantityInStock { get; set; }

}
