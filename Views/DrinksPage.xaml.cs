using DrinkIT.Models;
using DrinkIT.Services;
using DrinkIT.ViewModels;

namespace DrinkIT.Views;

public partial class DrinksPage : ContentPage
{
    public DrinksPage()
    {
        InitializeComponent();
        BindingContext = new DrinksViewModel(new DrinkService());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DrinksViewModel viewModel)
        {
            var dbService = new DatabaseService();
            var ingredients = await dbService.GetIngredientsAsync();
            await viewModel.LoadDrinksAsync(ingredients);
        }
    }

    private async void OnDrinkSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Drink selectedDrink)
        {
            await Shell.Current.GoToAsync($"DrinkDetailPage?drinkId={selectedDrink.IdDrink}");
        }
    }
}