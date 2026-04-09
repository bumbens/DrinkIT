using DrinkIT.Models;
using DrinkIT.Services;
using DrinkIT.ViewModels;

namespace DrinkIT.Views;

public partial class AllDrinksPage : ContentPage
{
    public AllDrinksPage()
    {
        InitializeComponent();
        BindingContext = new ListOfDrinksViewModel(new DrinkService());
        var letters = Enumerable.Range('A', 26)
            .Select(c => ((char)c).ToString())
            .ToList();
        letters.Add("Other");
        LetterPicker.ItemsSource = letters;    
    }
    private async void OnShowFilterClicked(object sender, EventArgs e)
    {
        if (BindingContext is ListOfDrinksViewModel viewModel)
        {
            viewModel.IsFilterVisible = true;
        }
    }
    private async void OnHideFilterClicked(object sender, EventArgs e)
    {
        if (BindingContext is ListOfDrinksViewModel viewModel)
        {
            viewModel.IsFilterVisible = false;
        }
    }
    private async void OnAllClicked(object sender, EventArgs e)
    {
        if (BindingContext is ListOfDrinksViewModel viewModel)
        {
            viewModel.AlcoholicStatusFilter = "All";
            viewModel.ApplyAlcoholicFilter();
        }
    }
    private async void OnFilterAlcoholicClicked(object sender, EventArgs e)
    {
        if (BindingContext is ListOfDrinksViewModel viewModel)
        {
            viewModel.AlcoholicStatusFilter = "Alcoholic";
            viewModel.ApplyAlcoholicFilter();
        }
    }
    private async void OnFilterNonAlcoholicClicked(object sender, EventArgs e)
    {
        if (BindingContext is ListOfDrinksViewModel viewModel)
        {
            viewModel.AlcoholicStatusFilter = "Non-alcoholic";
            viewModel.ApplyAlcoholicFilter();
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
    
    }


    private async void OnLetterSelected(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedItem is string selectedLetter)
        {
            if (BindingContext is ListOfDrinksViewModel viewModel)
            {
                if (selectedLetter == "Other")
                    await viewModel.LoadOtherDrinksAsync();
                else
                    await viewModel.LoadDrinksByLetterAsync(selectedLetter);
            }
        }
    }
    
    private async void OnDrinkSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ListOfDrinks selectedDrink)
        {
            await Shell.Current.GoToAsync($"DrinkDetailPage?drinkId={selectedDrink.IdDrink}");
        }
    }
}