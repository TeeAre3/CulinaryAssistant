using System.Collections.ObjectModel;
using System.Windows.Input;
using CulinaryAssistant.Models;
using CulinaryAssistant.Services;

namespace CulinaryAssistant.ViewModels
{
    public class MainViewModel : BindableObject 
    {
        private readonly RecipeService _recipeService;

        private string _searchQuery;
        private bool _isBusy;

        public ObservableCollection<Meal> Meals {  get; set; } = new ObservableCollection<Meal>();

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }

        public MainViewModel()
        {
            _recipeService = new RecipeService();
            SearchCommand = new Command(async () => await SearchMealsAsync());
        }

        private async Task SearchMealsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                await Application.Current.MainPage.DisplayAlert("Test", "Pole wyszukiwania jest puste", "OK");
                return;
            }

            IsBusy = true;

            Meals.Clear();

            var results = await _recipeService.GetMealsByIngredientAsync(SearchQuery);

            foreach (var meal in results)
            {
                Meals.Add(meal);
            }

            IsBusy = false;
        }
    }
}
