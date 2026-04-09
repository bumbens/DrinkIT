using DrinkIT.Models;
using DrinkIT.Services;
using System.Reflection;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DrinkIT.ViewModels;

public class DrinkDetailViewModel(DrinkService drinkService) : INotifyPropertyChanged
{
    private DrinkService _drinkService = drinkService;
    
    //Private field that stores a value
    private DrinkDetails? _drinkDetails;
    private bool _isImgLoading = true;
    private ImageSource? _drinkImage;
    
    //Event that the view can observe
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<RecipeIngredient> Ingredients { get; private set; } = [];
    public DrinkDetails? DrinkDetails
    {
        get {return _drinkDetails;}

        private set
        {
            _drinkDetails = value;
            PropertyChangedEventArgs args = new(nameof(DrinkDetails));

            if (PropertyChanged != null) {
                PropertyChanged.Invoke(this, args);
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(IsAlcoholicStatus)));
            }
        }
    }

    public async Task LoadDrinkDetailsAsync(string drinkId)
    {
        IsImgLoading = true;
        var details = await _drinkService.GetDrinkDetailsAsync(drinkId);
        if (details != null)
        {
            DrinkDetails = details;

            using var httpClient = new HttpClient();
            var imageBytes = await httpClient.GetByteArrayAsync(details.StrDrinkThumb);
            DrinkImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            IsImgLoading = false;

            for(int i = 1; i <= 15; i++){
                Type type = details.GetType();

                PropertyInfo? propertyIngredient = type.GetProperty($"StrIngredient{i}");
                PropertyInfo? propertyMeasure = type.GetProperty($"StrMeasure{i}");

                string ingredientName = "";
                string measure = "";

                if (propertyIngredient is not null && propertyMeasure is not null)
                {
                    object? ingredientValue = propertyIngredient.GetValue(details);
                    object? measureValue = propertyMeasure.GetValue(details);

                    if (ingredientValue != null )
                    {
                        ingredientName = ingredientValue.ToString() ?? "";
                    }
                    if (measureValue != null)
                    {
                        measure = measureValue?.ToString() ?? "";
                    }
                }

                if (!string.IsNullOrEmpty(ingredientName))
                {
                    Ingredients.Add(new RecipeIngredient { Name = ingredientName, Measure = measure });
                };
            }
            
        }
    }

    public ImageSource? DrinkImage
    {
        get { return _drinkImage; }
        set
        {
            _drinkImage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DrinkImage)));
        }
    }

    public bool? IsAlcoholic
    {
        get
        {
            if (DrinkDetails?.StrAlcoholic == null) return null;
            return DrinkDetails.StrAlcoholic.ToLower().Contains("alcoholic");
        }
    }

    public string IsAlcoholicStatus
    {
        get
        {
            if (DrinkDetails?.StrAlcoholic == null) return "Unknown";
            if (DrinkDetails.StrAlcoholic.ToLower().Contains("non")) return "Non-alcoholic";
            if (DrinkDetails.StrAlcoholic.ToLower().Contains("alcoholic")) return "Alcoholic";
            return "Unknown";
        }
    }

    public bool IsImgLoading
    {
        get { return _isImgLoading; }
        set { 
            _isImgLoading = value; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsImgLoading)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsImgLoaded)));}
    }

    public bool IsImgLoaded => !IsImgLoading;
}