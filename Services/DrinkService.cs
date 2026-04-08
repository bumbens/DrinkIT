namespace DrinkIT.Services;

using System.Text.Json;
using DrinkIT.Models;

public class DrinkService
{
    private HttpClient _httpClient;

    public DrinkService()
    {
        _httpClient = new HttpClient();
    }

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
            PropertyNameCaseInsensitive = true
    };

    public async Task<List<Drink>?> GetDrinksByIngredientAsync(string ingredient)
    {
        var url = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?i={ingredient}";
        var response = await _httpClient.GetStringAsync(url);
        var result = JsonSerializer.Deserialize<DrinkResponse>(response, _jsonOptions );
        return result?.Drinks;
    }

    public async Task<DrinkDetails?> GetDrinkDetailsAsync(string idDrink)
    {
        var url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={idDrink}";
        var response = await _httpClient.GetStringAsync(url);
        var result = JsonSerializer.Deserialize<DrinkDetailsResponse>(response, _jsonOptions);
        return result?.Drinks?.FirstOrDefault();
    }

    public async Task<List<IngredientsAPI>?> GetAvailableIngredientsAsync()
    {
        var url = $"https://www.thecocktaildb.com/api/json/v1/1/list.php?i=list";
        var response = await _httpClient.GetStringAsync(url);
        var result = JsonSerializer.Deserialize<IngredientsResponse>(response, _jsonOptions);
        return result?.Drinks;
    }

    public async Task<List<ListOfDrinks>?> GetDrinksByFirstLetterAsync(string letter)
    {
        var url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?f={letter}";
        var response = await _httpClient.GetStringAsync(url);
        var result = JsonSerializer.Deserialize<ListOfDrinksResponse>(response, _jsonOptions);
        return result?.Drinks;
    }

    public async Task<List<Drink>?> GetDrinksByIngredientsAsync(List<Ingredient> ingredients)
    {
        List<List<Drink>> allDrinks = [];
        foreach (var ingredient in ingredients)
        {
            var drinks = await GetDrinksByIngredientAsync(ingredient.Name ?? string.Empty);
            if (drinks != null)
            {
                allDrinks.Add(drinks);
            } 
        }
        
        if (allDrinks.Count == 0) return null;

        var commonDrinks = allDrinks[0];
        foreach (var drinkList in allDrinks.Skip(1))
        {
            commonDrinks = commonDrinks
                .Where(d => drinkList.Any(d2 => d2.IdDrink == d.IdDrink))
                .ToList();
        }
        return commonDrinks;
    }
}