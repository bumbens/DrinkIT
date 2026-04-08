using DrinkIT.Models;
using System.Collections.ObjectModel;

namespace DrinkIT.Services;

public class IngredientService
{
    private ObservableCollection<Ingredient> _ingredients = [];

    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        _ingredients.Remove(ingredient);
    }

    public void ClearAllIngredients()
    {
        _ingredients.Clear();
    }

    public ObservableCollection<Ingredient> GetAll()
    {
        return _ingredients;
    }



}