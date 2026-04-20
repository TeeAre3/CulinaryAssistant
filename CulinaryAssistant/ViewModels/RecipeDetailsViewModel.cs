using CulinaryAssistant.Models;
using CulinaryAssistant.Services;
using AutoMapper;
using CulinaryAssistant.Profiles;
using Microsoft.Extensions.Logging.Abstractions;

namespace CulinaryAssistant.ViewModels
{
    public class RecipeDetailsViewModel : BindableObject
    {
        private readonly RecipeService _recipeService;
        private readonly IMapper _mapper;

        private CleanRecipe? _recipe;
        private bool _isLoading;

        public CleanRecipe? Recipe
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

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecipeProfile>();
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();

            _ = LoadRecipeAsync(mealId);
        }

        private async Task LoadRecipeAsync(string mealId)
        {
            IsLoading = true;
            try
            {
                var rawRecipe = await _recipeService.GetMealDetailsByIdAsync(mealId);
                if(rawRecipe != null)
                { 
                    Recipe = _mapper.Map<CleanRecipe>(rawRecipe);
                }
            }
            catch (Exception)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert(
                    "Error",
                    $"Failed to load recipe details. Please check your internet conection.",
                    "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
