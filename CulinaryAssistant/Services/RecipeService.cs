using System.Net.Http.Json;
using CulinaryAssistant.Models;

namespace CulinaryAssistant.Services
{
    internal class RecipeService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://www.themealdb.com/api/json/v1/1/";

        public RecipeService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Meal>> GetMealsByIngredientAsync(string ingredient)
        {
            try
            {
                string url = $"{BaseUrl}filter.php?i={ingredient}";
                var response = await _httpClient.GetFromJsonAsync<MealApiResponse>(url);
                return response?.Meals ?? new List<Meal>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd pobierania danych: {ex.Message}");
                return new List<Meal>();
            }
        }

        public async Task<MealDetail?> GetMealDetailsByIdAsync(string mealId)
        {
            try
            {
                string url = $"{BaseUrl}lookup.php?i={mealId}";

                var response = await _httpClient.GetFromJsonAsync<MealDetailApiResponse>(url);

                return response?.Meals?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd pobierania danych: {ex.Message}");
                return null;
            }
        }
    }
}
