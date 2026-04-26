using CulinaryAssistant.ViewModels;
using CulinaryAssistant.Models;

namespace CulinaryAssistant.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    public FavoritesPage()
    {
        InitializeComponent();
        _viewModel = new FavoritesViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _viewModel.LoadFavoritesAsync();
    }

    private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is CleanRecipe selectedMeal && selectedMeal.Id != null)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new RecipeDetailsPage(selectedMeal.Id));
        }
    }
}