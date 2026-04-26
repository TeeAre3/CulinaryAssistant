namespace CulinaryAssistant.Services
{
    public class FavoritesService
    {
        private const string FavoritesKey = "FavoriteMeals";

        public List<string> GetFavoriteIds()
        {
            var savedData = Preferences.Get(FavoritesKey, string.Empty);
            if (string.IsNullOrWhiteSpace(savedData)) return new List<string>();

            return savedData.Split(',').ToList();
        }

        public void AddToFavorites(string mealId)
        {
            var favorites = GetFavoriteIds();
            if (!favorites.Contains(mealId))
            {
                favorites.Add(mealId);
                SaveFavorites(favorites);
            }
        }

        public void RemoveFromFavorites(string mealId)
        {
            var favorites = GetFavoriteIds();
            if (favorites.Contains(mealId))
            {
                favorites.Remove(mealId);
                SaveFavorites(favorites);
            }
        }

        private void SaveFavorites(List<string> favorites)
        {
            var joinedString = string.Join(",", favorites);
            Preferences.Set(FavoritesKey, joinedString);
        }

        public bool IsFavorite(string mealId)
        {
            return GetFavoriteIds().Contains(mealId);
        }
    }
}