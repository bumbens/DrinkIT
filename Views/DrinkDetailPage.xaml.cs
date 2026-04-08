using DrinkIT.Models;
using DrinkIT.Services;
using DrinkIT.ViewModels;

namespace DrinkIT.Views;

public partial class DrinkDetailPage : ContentPage, IQueryAttributable
{

    private string _drinkId = "";
    public DrinkDetailPage()
    {
        InitializeComponent();
        BindingContext = new DrinkDetailViewModel(new DrinkService());
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {

        if (query.TryGetValue("drinkId", out var idObj))
        {
            _drinkId = idObj.ToString() ?? "";

        }
    }

    private void OnImgLoaded(object sender, EventArgs e)
    {
            Console.WriteLine("IMAGE LOADED");

        if (BindingContext is DrinkDetailViewModel viewModel)
        {
            viewModel.IsImgLoading = false;
        }
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DrinkDetailViewModel viewModel && !string.IsNullOrEmpty(_drinkId))
            await viewModel.LoadDrinkDetailsAsync(_drinkId);
    }

}
