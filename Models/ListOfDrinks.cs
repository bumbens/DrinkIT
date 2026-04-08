namespace DrinkIT.Models;
public class ListOfDrinks
{
    public string? StrDrink { get; set; }
    public string? StrAlcoholic { get; set; }
    public string? IdDrink { get; set; }

}

public class ListOfDrinksResponse
{
    public List<ListOfDrinks>? Drinks { get; set; }
}