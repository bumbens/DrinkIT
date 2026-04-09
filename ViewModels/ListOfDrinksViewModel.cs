using DrinkIT.Models;
using DrinkIT.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DrinkIT.ViewModels;

public class ListOfDrinksViewModel(DrinkService drinkService) : INotifyPropertyChanged
{
    private DrinkService _drinkService = drinkService;
    public event PropertyChangedEventHandler? PropertyChanged;
    string SelectedLetter = ""; 
    private List<ListOfDrinks> _allDrinks = [];
    private bool _isFilterVisible = false;

    public ObservableCollection<ListOfDrinks> AllDrinks
    {
        get; set;
    } = [];

    public string AlcoholicStatusFilter { get; set; } = "All";
    public bool IsFilterVisible
    {
        get => _isFilterVisible;
        set
        {
            _isFilterVisible = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFilterVisible)));}
    }
    public void ApplyAlcoholicFilter()
    {
        var filtered = _allDrinks.Where(d => AlcoholicStatusFilter == "All" || 
            (AlcoholicStatusFilter == "Alcoholic" && d.StrAlcoholic?.ToLower().Contains("alcoholic") == true)||
            (AlcoholicStatusFilter == "Non-alcoholic" && d.StrAlcoholic?.ToLower().Contains("non") == true)).ToList();

        AllDrinks.Clear();
        foreach (var drink in filtered)
        {
            AllDrinks.Add(drink);
        }
    }

    private async Task AddDrinksByLetterAsync(string letter)
    {
        var drinks = await _drinkService.GetDrinksByFirstLetterAsync(letter);
        if (drinks == null) return;
        foreach (var drink in drinks)
        {
            _allDrinks.Add(drink);
        }
    }
    public async Task LoadOtherDrinksAsync()
    {
        _allDrinks.Clear();
        for (char c = '0'; c <= '9'; c++)
        {
            await AddDrinksByLetterAsync(c.ToString());
        }
        ApplyAlcoholicFilter();
    }
    public async Task LoadDrinksByLetterAsync(string letter)
    {
        _allDrinks.Clear();
        await AddDrinksByLetterAsync(letter);
        ApplyAlcoholicFilter();
    }

    
}