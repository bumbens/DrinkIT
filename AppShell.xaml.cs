using DrinkIT.Views;

namespace DrinkIT;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(DrinksPage), typeof(DrinksPage));
		Routing.RegisterRoute(nameof(DrinkDetailPage), typeof(DrinkDetailPage));
		Routing.RegisterRoute(nameof(AllDrinksPage), typeof(AllDrinksPage));
	}
}
