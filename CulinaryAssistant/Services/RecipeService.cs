using CulinaryAssistant.Models;
using System.Net.Http.Json;
using System.Text.Json;

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

        public async Task<List<string>> GetAreasAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("areas.json");
                using var reader = new StreamReader(stream);

                var json = await reader.ReadToEndAsync();

                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu pliku areas.json: {ex.Message}");
                return new List<string> { "All" }; 
            }
        }

        public async Task<List<Meal>> GetMealsByAreaAsync(string area)
        {
            try
            {
                if (string.IsNullOrEmpty(area) || area == "All")
                    return new List<Meal>();

                string url = $"{BaseUrl}filter.php?a={area}";
                var response = await _httpClient.GetFromJsonAsync<MealApiResponse>(url);
                return response?.Meals ?? new List<Meal>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd pobierania danych po regionie: {ex.Message}");
                return new List<Meal>();
            }
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

        public async Task<List<Meal>> GetMealsByCategoryAsync(string category)
        {
            try
            {
                string url = $"{BaseUrl}filter.php?c={category}";
                var response = await _httpClient.GetFromJsonAsync<MealApiResponse>(url);
                return response?.Meals ?? new List<Meal>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find category: {ex.Message}");
                return new List<Meal>();
            }
        }
    }
}
