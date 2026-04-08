using DrinkIT.ViewModels;
using DrinkIT.Services;
using DrinkIT.Models;


namespace DrinkIT.Views;


public partial class IngredientsPage : ContentPage
{
    public IngredientsPage()
    {
        InitializeComponent();
        BindingContext = new IngredientViewModel(new DatabaseService(), new DrinkService());
    }

    private async void OnFindClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new DrinksPage());
    }

    private async void OnAllDrinksClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AllDrinksPage());
    }

    private async void OnClearAllClicked(object? sender, EventArgs e)
    {
        if (BindingContext is not IngredientViewModel viewModel) return;
        await viewModel.ClearAllIngredients();
    }

    private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (BindingContext is not IngredientViewModel viewModel) return;
        viewModel.FilterIngredients(e.NewTextValue);
    }

    private async void OnAvailableIngredientSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is IngredientsAPI selectedIngredient)
        {
            if (BindingContext is not IngredientViewModel viewModel) return;
            var ingredient = new Ingredient { Name = selectedIngredient.StrIngredient1, IsAvailable = true };
            await viewModel.AddIngredient(ingredient);
            await IngredientEntry.HideSoftInputAsync(CancellationToken.None);
            IngredientEntry.Text = string.Empty;
        }
    }

    private async void OnIngredientSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Ingredient selectedIngredient)
        {
            if (BindingContext is not IngredientViewModel viewModel) return;
            await viewModel.RemoveIngredient(selectedIngredient);
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is IngredientViewModel viewModel)
        {
            await viewModel.LoadIngredientsAsync();
            await viewModel.LoadAvailableIngredientsAsync();
        }
    }
}

