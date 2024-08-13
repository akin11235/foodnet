using System;
using Core.Entities;

namespace Core.Specifications;

public class MenuItemSpecification : BaseSpecification<MenuItem>
{
    public MenuItemSpecification(string? mealtime, string? mealtype, string? sort) : base(x =>
    (string.IsNullOrWhiteSpace(mealtime) || x.MealTime == mealtime) &&
    (string.IsNullOrWhiteSpace(mealtype) || x.MealType == mealtype)
    ) 
    {
        switch (sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;

            case "priceDesc":
                AddOrderBDescending(x => x.Price);
                break;
            
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }

}
