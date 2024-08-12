using System;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using infrastructure.Config;

namespace infrastructure.Data;

public class RestaurantContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MealConfiguration).Assembly);
    }
}

