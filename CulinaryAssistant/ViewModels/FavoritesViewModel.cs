using System.Collections.ObjectModel;
using CulinaryAssistant.Models;
using CulinaryAssistant.Services;
using AutoMapper;
using CulinaryAssistant.Profiles;
using Microsoft.Extensions.Logging.Abstractions;

namespace CulinaryAssistant.ViewModels
{
    public class FavoritesViewModel : BindableObject
    {
        private readonly FavoritesService _favoritesService;
        private readonly RecipeService _recipeService;
        private readonly IMapper _mapper;
        private bool _isBusy;

        public ObservableCollection<CleanRecipe> FavoriteMeals { get; set; } = new();

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public FavoritesViewModel()
        {
            _favoritesService = new FavoritesService();
            _recipeService = new RecipeService();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<RecipeProfile>(), NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();
        }

        public async Task LoadFavoritesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            FavoriteMeals.Clear();

            try
            {
                var favoriteIds = _favoritesService.GetFavoriteIds();
                foreach (var id in favoriteIds)
                {
                    var rawRecipe = await _recipeService.GetMealDetailsByIdAsync(id);
                    if (rawRecipe != null)
                    {
                        var cleanRecipe = _mapper.Map<CleanRecipe>(rawRecipe);
                        FavoriteMeals.Add(cleanRecipe);
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}