using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class MealRepository(RestaurantContext context) : IMenuItemRepository
{
    public void AddMeal(MenuItem menuItem)
    {
        context.MenuItems.Add(menuItem);
    }

    public void DeleteMeal(MenuItem menuItem)
    {
        context.MenuItems.Remove(menuItem);
    }

    public async Task<MenuItem?> GetMealByIdAsync(int id)
    {
        return await context.MenuItems.FindAsync(id);
    }

    public async Task<IReadOnlyList<MenuItem>> GetMealsAsync(string? mealtime, string? mealtype, string? sort)
    {
       var query = context.MenuItems.AsQueryable();

       if(!string.IsNullOrWhiteSpace(mealtime))
            query = query.Where(x => x.MealTime == mealtime );

        if(!string.IsNullOrWhiteSpace(mealtype))
            query = query.Where(x => x.MealType == mealtype );


        query = sort switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Name)
            
        };

       return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetMealTimesAsync()
    {
        return await context.MenuItems.Select(x => x.MealTime)
        .Distinct()
        .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetMealTypesAsync()
    {
        return await context.MenuItems.Select(x => x.MealType)
        .Distinct()
        .ToListAsync();
    }

    public bool MealExists(int id)
    {
        return context.MenuItems.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateMeal(MenuItem menuItem)
    {
        context.Entry(menuItem).State = EntityState.Modified;
    }
}
