namespace CulinaryAssistant.Models
{
    public class IngredientItem
    {
        public string? Name { get; set; }
        public string? Measure { get; set; }
    }

    public class CleanRecipe
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public string? Area { get; set; }
        public string? Instructions { get; set; }

        public List<IngredientItem> Ingredients { get; set; } = new();
    }
}