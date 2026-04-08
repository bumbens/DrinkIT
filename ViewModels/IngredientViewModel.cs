using DrinkIT.Models;
using DrinkIT.Services;
using System.Collections.ObjectModel;

namespace DrinkIT.ViewModels;

public class IngredientViewModel(DatabaseService dbService, DrinkService drinkService)
{
    private DatabaseService _dbService = dbService;

    public ObservableCollection<Ingredient> Ingredients
    {
        get; set;
    } = [];

    public ObservableCollection<IngredientsAPI> IngredientsAPI
    {
        get; set;
    } = [];

    public ObservableCollection<IngredientsAPI> FilteredIngredients
    {
        get; set;
    } = [];

    public async Task LoadAvailableIngredientsAsync()
    {
        var ingredients = await drinkService.GetAvailableIngredientsAsync();
        IngredientsAPI.Clear();
        if (ingredients == null) return;
        foreach (var item in ingredients)
        {
            IngredientsAPI.Add(item);
        }
    }

    public void FilterIngredients(string query)
    {
        FilteredIngredients.Clear();
        if (string.IsNullOrEmpty(query)) return;
        foreach (var item in IngredientsAPI)
        {
            if (item.StrIngredient1 != null && item.StrIngredient1.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                FilteredIngredients.Add(item);
            }
        }

    }
    public async Task AddIngredient(Ingredient ingredient)
    {
        await _dbService.AddIngredientsAsync(ingredient);
        await LoadIngredientsAsync();
    }

    public async Task RemoveIngredient(Ingredient ingredient)
    {
        await _dbService.RemoveIngredientsAsync(ingredient);
        await LoadIngredientsAsync();

    }

    public async Task ClearAllIngredients(){
        await _dbService.ClearAllIngredientsAsync();
        await LoadIngredientsAsync();
    }

    public async Task LoadIngredientsAsync()
    {
        var ingredients = await _dbService.GetIngredientsAsync();
        Ingredients.Clear();
        foreach (var item in ingredients)
        {
            Ingredients.Add(item);
        }
    }
}

