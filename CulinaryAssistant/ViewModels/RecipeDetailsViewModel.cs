using CulinaryAssistant.Models;
using CulinaryAssistant.Services;

namespace CulinaryAssistant.ViewModels
{
    public class RecipeDetailsViewModel : BindableObject
    {
        private readonly RecipeService _recipeService;
        private MealDetail _recipe;
        private bool _isLoading;

        public MealDetail Recipe
        {
            get => _recipe;
            set
            {
                _recipe = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public RecipeDetailsViewModel(string mealId)
        {
            _recipeService = new RecipeService();
            LoadRecipeAsync(mealId);
        }

        private async void LoadRecipeAsync(string mealId)
        {
            IsLoading = true;
            Recipe = await _recipeService.GetMealDetailsByIdAsync(mealId);
            IsLoading = false;
        }
    }
}
