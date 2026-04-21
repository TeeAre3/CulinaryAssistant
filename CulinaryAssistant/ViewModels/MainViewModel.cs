using System.Collections.ObjectModel;
using System.Windows.Input;
using CulinaryAssistant.Models;
using CulinaryAssistant.Services;

namespace CulinaryAssistant.ViewModels
{
    public class MainViewModel : BindableObject 
    {
        private readonly RecipeService _recipeService;

        private string _searchQuery = string.Empty;
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
                await Application.Current!.Windows[0].Page!.DisplayAlert("Warning", "Search field is empty", "OK");
                return;
            }

            IsBusy = true;
            Meals.Clear();

            try
            { 
                var results = await _recipeService.GetMealsByIngredientAsync(SearchQuery);

                if(results == null || results.Count == 0)
                {
                    await Application.Current!.Windows[0].Page!.DisplayAlert("No results", "Could not fetch data. Check your internet connection or try another ingredient.", "OK");
                }
                else
                {
                    var sortedResults = results.OrderBy(meal => meal.Title).ToList();

                    foreach (var meal in sortedResults)
                    {
                        Meals.Add(meal);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("System Error", ex.Message, "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
