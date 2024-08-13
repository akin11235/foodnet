using System;
using Core.Entities;

namespace Core.Specifications;

public class MealTypeSpecification : BaseSpecification<MenuItem, string>
{
    public MealTypeSpecification()
    {
        AddSelect(x => x.MealType);
        ApplyDistinct();
    }

}
