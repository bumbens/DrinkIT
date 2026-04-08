namespace DrinkIT.Models;

public class Drink
{
    public string? StrDrink { get; set; } 
    public string? StrDrinkThumb { get; set; }
    public string? IdDrink { get; set; }
}

public class DrinkResponse
{
    public List<Drink>? Drinks { get; set; }
}