using System.Collections.ObjectModel;
using System.Windows.Input;
using CulinaryAssistant.Models;
using CulinaryAssistant.Services;
using CulinaryAssistant.Helpers;

namespace CulinaryAssistant.ViewModels
{
    public class MainViewModel : BindableObject 
    {
        private readonly RecipeService _recipeService;

        private string _searchQuery = string.Empty;
        private bool _isBusy;
        private string _selectedArea = "Random";

        public ObservableCollection<Meal> Meals {  get; set; } = new ObservableCollection<Meal>();
        public ObservableCollection<string> Areas { get; set; } = new ObservableCollection<string>();

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public string SelectedArea
        {
            get => _selectedArea;
            set
            {
                if (_selectedArea != value)
                {
                    _selectedArea = value;
                    OnPropertyChanged();
                    _ = SearchByAreaAsync();
                }
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
            _ = LoadAreasAsync();
            _ = SearchByAreaAsync();
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
            _selectedArea = "All";
            OnPropertyChanged(nameof(SelectedArea));

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

        private async Task LoadAreasAsync()
        {
            var list = await _recipeService.GetAreasAsync();
            foreach (var area in list)
            {
                Areas.Add(area);
            }
        }

        private async Task SearchByAreaAsync()
        {
            IsBusy = true;
            Meals.Clear();
            SearchQuery = string.Empty;

            try
            {
                List<Meal> results;

                if (string.IsNullOrEmpty(SelectedArea) || SelectedArea == "Random")
                {
                    string randomCategory = AppConstants.AvailableCategories
                                                                .OrderBy(x => Guid.NewGuid())
                                                                .First();

                    results = await _recipeService.GetMealsByCategoryAsync(randomCategory);
                }
                else
                {
                    results = await _recipeService.GetMealsByAreaAsync(SelectedArea);
                }

                if(results != null)
                {
                    foreach (var meal in results)
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
