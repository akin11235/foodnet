using System;
using Core.Entities;
using Core.Interfaces;
using infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MenuItemsController(IMenuItemRepository repo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MenuItem>>> GetMeals(string? mealtime, 
        string? mealtype, string? sort)
    {
        return Ok(await repo.GetMealsAsync(mealtime, mealtype, sort));
    }

    [HttpGet("{id:int}")] //api/meals/2
    public async Task<ActionResult<MenuItem>> GetMeal(int id)
    {
        var menuItem = await repo.GetMealByIdAsync(id);
        if(menuItem == null) return NotFound();
        return menuItem;
    }

    [HttpPost]
    public async Task<ActionResult<MenuItem>> CreateMeal(MenuItem menuItem)
    {
        repo.AddMeal(menuItem);
        if(await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetMeal", new{id = menuItem.Id}, menuItem);
        }
        return BadRequest("Problem creating meal");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateMeal(int id, MenuItem menuItem)
    {
        if (menuItem.Id != id || !MealExists(id)) 
        return BadRequest("Cannot update this meal");

        repo.UpdateMeal(menuItem);

        if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the meal");

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMeal(int id)
    {
        var meal = await repo.GetMealByIdAsync(id);

        if(meal == null) return NotFound();

        repo.DeleteMeal(meal);

        if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the meal");
    }

    [HttpGet("mealtypes")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetMealTypes()
    {
        return Ok(await repo.GetMealTypesAsync());
    }

    [HttpGet("mealtimes")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetMealTimes()
    {
        return Ok(await repo.GetMealTimesAsync());
    }

    private bool MealExists(int id)
    {
        return repo.MealExists(id);
    }


}
