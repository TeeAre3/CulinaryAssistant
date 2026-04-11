using CulinaryAssistant.Models;
using CulinaryAssistant.ViewModels;
using CulinaryAssistant.Views;

namespace CulinaryAssistant
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.FirstOrDefault() is Meal selectedMeal)
            {
                ((CollectionView)sender).SelectedItem = null;

                await Navigation.PushAsync(new RecipeDetailsPage(selectedMeal.Id));
            }
        }
    }
}
