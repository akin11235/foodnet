using System;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MenuItemsController(IGenericRepository<MenuItem> repo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MenuItem>>> GetMeals(string? mealtime, 
        string? mealtype, string? sort)
    {

        var spec = new MenuItemSpecification(mealtime, mealtype, sort);

        var meals = await repo.ListAsync(spec);
        return Ok(meals);
    }

    [HttpGet("{id:int}")] //api/meals/2
    public async Task<ActionResult<MenuItem>> GetMeal(int id)
    {
        var menuItem = await repo.GetByIdAsync(id);
        if(menuItem == null) return NotFound();
        return menuItem;
    }

    [HttpPost]
    public async Task<ActionResult<MenuItem>> CreateMeal(MenuItem menuItem)
    {
        repo.Add(menuItem);
        if(await repo.SaveAllAsync())
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

        repo.Update(menuItem);

        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the meal");

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMeal(int id)
    {
        var meal = await repo.GetByIdAsync(id);

        if(meal == null) return NotFound();

        repo.Remove(meal);

        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the meal");
    }

    [HttpGet("mealtypes")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetMealTypes()
    {
        var spec = new MealTypeSpecification();
        
        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("mealtimes")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetMealTimes()
    {
        var spec = new MealTimeSpecification();
        
        return Ok(await repo.ListAsync(spec));
    }

    private bool MealExists(int id)
    {
        return repo.Exists(id);
    }
}
