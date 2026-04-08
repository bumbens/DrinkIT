using DrinkIT.Models;
using DrinkIT.Services;
using System.Collections.ObjectModel;

namespace DrinkIT.ViewModels;

public class ListOfDrinksViewModel(DrinkService drinkService)
{
    private DrinkService _drinkService = drinkService;
    string SelectedLetter = ""; 

    public ObservableCollection<ListOfDrinks> AllDrinks
    {
        get; set;
    } = [];

    private async Task AddDrinksByLetterAsync(string letter)
    {
        var drinks = await _drinkService.GetDrinksByFirstLetterAsync(letter);
        if (drinks == null) return;
        foreach (var drink in drinks)
        {
            AllDrinks.Add(drink);
        }
    }
    public async Task LoadOtherDrinksAsync()
    {
        AllDrinks.Clear();
        for (char c = '0'; c <= '9'; c++)
        {
            await AddDrinksByLetterAsync(c.ToString());
        }
    }
    public async Task LoadDrinksByLetterAsync(string letter)
    {
        AllDrinks.Clear();
        await AddDrinksByLetterAsync(letter);
    }

    
}