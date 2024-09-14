using System;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace infrastructure.Data;

public class RestaurantContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Address> Addresses {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MealConfiguration).Assembly);
    }
}

