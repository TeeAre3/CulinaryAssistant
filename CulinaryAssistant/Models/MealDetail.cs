using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CulinaryAssistant.Models
{
    public class MealDetail
    {
        [JsonPropertyName("idMeal")]
        public string Id { get; set; }

        [JsonPropertyName("strMeal")]
        public string Title { get; set; }
        [JsonPropertyName("strMealThumb")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("strCategory")]
        public string Category { get; set; }
        [JsonPropertyName("strInstructions")]
        public string Instructions { get; set; }
        [JsonPropertyName("strYoutube")]
        public string YoutubeUrl { get; set; }

        [JsonPropertyName("strIngredient1")]
        public string Ingredient1 { get; set; }
        [JsonPropertyName("strIngredient2")]
        public string Ingredient2 { get; set; }
        [JsonPropertyName("strIngredient3")]
        public string Ingredient3 { get; set; }
        [JsonPropertyName("strIngredient4")]
        public string Ingredient4 { get; set; }
        [JsonPropertyName("strIngredient5")]
        public string Ingredient5 { get; set; }
        [JsonPropertyName("strIngredient6")]
        public string Ingredient6 { get; set; }
        [JsonPropertyName("strIngredient7")]
        public string Ingredient7 { get; set; }
        [JsonPropertyName("strIngredient8")]
        public string Ingredient8 { get; set; }
        [JsonPropertyName("strIngredient9")]
        public string Ingredient9 { get; set; }
        [JsonPropertyName("strIngredient10")]
        public string Ingredient10 { get; set; }
        [JsonPropertyName("strIngredient11")]
        public string Ingredient11 { get; set; }
        [JsonPropertyName("strIngredient12")]
        public string Ingredient12 { get; set; }
        [JsonPropertyName("strIngredient13")]
        public string Ingredient13 { get; set; }
        [JsonPropertyName("strIngredient14")]
        public string Ingredient14 { get; set; }
        [JsonPropertyName("strIngredient15")]
        public string Ingredient15 { get; set; }
        [JsonPropertyName("strIngredient16")]
        public string Ingredient16 { get; set; }
        [JsonPropertyName("strIngredient17")]
        public string Ingredient17 { get; set; }
        [JsonPropertyName("strIngredient18")]
        public string Ingredient18 { get; set; }
        [JsonPropertyName("strIngredient19")]
        public string Ingredient19 { get; set; }
        [JsonPropertyName("strIngredient20")]
        public string Ingredient20 { get; set; }
    }

    public class MealDetailApiResponse
    {
        [JsonPropertyName("meals")]
        public List<MealDetail> Meals { get; set; }
    }
}
