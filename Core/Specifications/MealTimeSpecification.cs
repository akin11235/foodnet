using System;
using Core.Entities;

namespace Core.Specifications;

public class MealTimeSpecification : BaseSpecification<MenuItem, string>
{
    public MealTimeSpecification()
    {
        AddSelect(x => x.MealTime);
        ApplyDistinct();
        
    }

}
