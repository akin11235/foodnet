using System;
using Core.Entities;

namespace Core.Specifications;

public class MenuItemSpecification : BaseSpecification<MenuItem>
{
    public MenuItemSpecification(MenuItemSpecParams specParams) : base(x =>
    (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
    (specParams.MealTimes.Count == 0 || specParams.MealTimes.Contains(x.MealTime)) &&
    (specParams.MealTypes.Count == 0 || specParams.MealTypes.Contains(x.MealType))
    ) 
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        switch (specParams.Sort)
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
