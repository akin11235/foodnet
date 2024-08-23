using System;

namespace Core.Specifications;

public class MenuItemSpecParams
{

    private const int MaxPageSize = 50;
    public int PageIndex { get; set; }  = 1;

    private int _pageSize = 6;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    

    private List<string> _mealTypes = [];
    public List<string> MealTypes
    {
        get => _mealTypes;
        set
        {
            _mealTypes =value.SelectMany(x => x.Split(',', 
                StringSplitOptions.RemoveEmptyEntries)).ToList();

        }
    }
    
    private List<string> _mealTimes = [];
    public List<string> MealTimes
    {
        get => _mealTimes;
        set
        {
            _mealTimes =value.SelectMany(x => x.Split(',', 
                StringSplitOptions.RemoveEmptyEntries)).ToList();

        }
    }

    public string? Sort { get; set; }

    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }
    

}
