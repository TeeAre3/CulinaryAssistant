using System.Text.Json;
using System.Text.Json.Serialization;

namespace CulinaryAssistant.Models
{
    public class MealDetail
    {
        [JsonPropertyName("idMeal")]
        public string? Id { get; set; }

        [JsonPropertyName("strMeal")]
        public string? Title { get; set; }

        [JsonPropertyName("strMealThumb")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("strCategory")]
        public string? Category { get; set; }

        [JsonPropertyName("strArea")]
        public string? Area { get; set; }

        [JsonPropertyName("strInstructions")]
        public string? Instructions { get; set; }

        [JsonPropertyName("strYoutube")]
        public string? YoutubeUrl { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement>? OverflowData { get; set; }

        public List<IngredientItem> ExtractIngredients()
        {
            return Enumerable.Range(1,20)
                .Select(i => new
                {
                    Name = OverflowData?[$"strIngredient{i}"].GetString(),
                    Measure = OverflowData?[$"strMeasure{i}"].GetString()
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .Select(x => new IngredientItem { Name = x.Name!, Measure = x.Measure ?? "" })
                .ToList();
        }
    }

    public class MealDetailApiResponse
    {
        [JsonPropertyName("meals")]
        public List<MealDetail>? Meals { get; set; }
    }
}