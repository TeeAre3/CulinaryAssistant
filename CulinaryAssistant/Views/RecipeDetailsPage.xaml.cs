using CulinaryAssistant.ViewModels;

namespace CulinaryAssistant.Views;

public partial class RecipeDetailsPage : ContentPage
{
	public RecipeDetailsPage(string mealId)
	{
		InitializeComponent();
		BindingContext = new CulinaryAssistant.ViewModels.RecipeDetailsViewModel(mealId);
	}
}