using System.Text.Json.Serialization;

namespace CulinaryAssistant.Models
{
    public class Meal
    {
        [JsonPropertyName("idMeal")]
        public string Id { get; set; }
        [JsonPropertyName("strMeal")]
        public string Title { get; set; }
        [JsonPropertyName("strMealThumb")]
        public string ImageUrl { get; set; }
    }

    public class MealApiResponse
    {
        [JsonPropertyName("meals")]
        public List<Meal> Meals { get; set; }
    }
}
