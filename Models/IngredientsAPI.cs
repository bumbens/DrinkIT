namespace DrinkIT.Models;

public class IngredientsAPI
{
    public string? StrIngredient1 { get; set; }
}

public class IngredientsResponse
{
    public List<IngredientsAPI>? Drinks { get; set; }
}