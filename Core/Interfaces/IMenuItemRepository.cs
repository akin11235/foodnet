using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IMenuItemRepository
{
    Task<IReadOnlyList<MenuItem>> GetMealsAsync(string? mealtime, string? mealtype, string? sort);
    Task<MenuItem?> GetMealByIdAsync(int id);
    Task<IReadOnlyList<string>> GetMealTypesAsync();

    Task<IReadOnlyList<string>> GetMealTimesAsync();
    void AddMeal(MenuItem meal);
    void UpdateMeal(MenuItem meal);
    void DeleteMeal(MenuItem meal);

    bool MealExists(int id);
    
    Task<bool> SaveChangesAsync();
    


}
