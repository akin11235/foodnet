using System;
using Core.Entities;
using infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MealsController : ControllerBase
{
    private readonly RestaurantContext context;

    public MealsController(RestaurantContext context)
    {
        this.context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
    {
        return await context.Meals.ToListAsync();
    }

    [HttpGet("{id:int}")] //api/meals/2
    public async Task<ActionResult<Meal>> GetMeal(int id)
    {
        var meal = await context.Meals.FindAsync(id);
        if(meal == null) return NotFound();
        return meal;
    }

    [HttpPost]
    public async Task<ActionResult<Meal>> CreateMeal(Meal meal)
    {
        context.Meals.Add(meal);
        await context.SaveChangesAsync();
        return meal;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateMeal(int id, Meal meal)
    {
        if (meal.Id != id || !MealExists(id)) 
        return BadRequest("Cannot update this meal");

        context.Entry(meal).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMeal(int id)
    {
        var meal = await context.Meals.FindAsync(id);

        if(meal == null) return NotFound();

        context.Meals.Remove(meal);

        await context.SaveChangesAsync();
        return NoContent();
    }

    private bool MealExists(int id)
    {
        return context.Meals.Any(x => x.Id == id);
    }


}
