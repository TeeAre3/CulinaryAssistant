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
        public Dictionary<string, JsonElement> OverflowData { get; set; }

        public List<IngredientItem> ExtractIngredients()
        {
            var ingredientsList = new List<IngredientItem>();

            for (int i = 1; i <= 20; i++)
            {
                string ingredientKey = $"strIngredient{i}";
                string measureKey = $"strMeasure{i}";

                if (OverflowData != null && OverflowData.ContainsKey(ingredientKey))
                {
                    string name = OverflowData[ingredientKey].GetString();

                    string measure = "";
                    if (OverflowData.ContainsKey(measureKey) && OverflowData[measureKey].ValueKind != JsonValueKind.Null)
                    {
                        measure = OverflowData[measureKey].GetString();
                    }

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        ingredientsList.Add(new IngredientItem { Name = name, Measure = measure });
                    }
                }
            }

            return ingredientsList;
        }
    }

    public class MealDetailApiResponse
    {
        [JsonPropertyName("meals")]
        public List<MealDetail>? Meals { get; set; }
    }
}