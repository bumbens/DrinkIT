using DrinkIT.Models;
using DrinkIT.Services;
using System.Collections.ObjectModel;

namespace DrinkIT.ViewModels;

public class DrinksViewModel(DrinkService drinkService)
{
    private DrinkService _drinkService = drinkService;

    public ObservableCollection<Drink> Drinks
    {
        get; set;
    } = new();

    public async Task LoadDrinksAsync(List<Ingredient> ingredients)
    {
        var drinks = await _drinkService.GetDrinksByIngredientsAsync(ingredients);
        if (drinks == null) return;
        Drinks.Clear();
        foreach (var drink in drinks)
        {
            Drinks.Add(drink);
        }
    }
}