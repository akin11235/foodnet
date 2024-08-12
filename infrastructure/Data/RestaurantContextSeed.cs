using System;
using System.Text.Json;
using Core.Entities;

namespace infrastructure.Data;

public class RestaurantContextSeed
{
    public static async Task SeedAsync(RestaurantContext context)
    {
     
        if(!context.MenuItems.Any())
        {
            var mealsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/meals.json");

            var meals = JsonSerializer.Deserialize<List<MenuItem>>(mealsData);

            if (meals == null) return;

            context.MenuItems.AddRange(meals);
            await context.SaveChangesAsync();
        }
    }

}
