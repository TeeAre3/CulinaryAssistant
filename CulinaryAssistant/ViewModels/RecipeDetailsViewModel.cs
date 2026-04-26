using CulinaryAssistant.Models;
using CulinaryAssistant.Services;
using AutoMapper;
using CulinaryAssistant.Profiles;
using Microsoft.Extensions.Logging.Abstractions;
using System.Windows.Input;

namespace CulinaryAssistant.ViewModels
{
    public class RecipeDetailsViewModel : BindableObject
    {
        private readonly RecipeService _recipeService;
        private readonly FavoritesService _favoritesService;
        private readonly IMapper _mapper;

        private CleanRecipe? _recipe;
        private bool _isLoading;
        private bool _isFavorite;

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

        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                _isFavorite = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FavoriteButtonText));
            }
        }

        public string FavoriteButtonText => IsFavorite ? "Remove from Favorites" : "Add to favorites";
        public ICommand ToggleFavoriteCommand { get; }

        public RecipeDetailsViewModel(string mealId)
        {
            _recipeService = new RecipeService();
            _favoritesService = new FavoritesService();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecipeProfile>();
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();

            ToggleFavoriteCommand = new Command(ToggleFavorite);

            _ = LoadRecipeAsync(mealId);
        }

        private void ToggleFavorite()
        {
            if (Recipe?.Id == null) return;

            if(IsFavorite)
            {
                _favoritesService.RemoveFromFavorites(Recipe.Id);
                IsFavorite = false;
            }
            else
            {
                _favoritesService.AddToFavorites(Recipe.Id);
                IsFavorite = true;
            }
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

                    if(Recipe.Id != null)
                    {
                        IsFavorite = _favoritesService.IsFavorite(Recipe.Id);
                    }
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
