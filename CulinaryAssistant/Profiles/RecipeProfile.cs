using AutoMapper;
using CulinaryAssistant.Models;

namespace CulinaryAssistant.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<MealDetail, CleanRecipe>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src =>
                    new List<IngredientItem>
                    {
                        new IngredientItem { Name = src.Ingredient1, Measure = src.Measure1 },
                        new IngredientItem { Name = src.Ingredient2, Measure = src.Measure2 },
                        new IngredientItem { Name = src.Ingredient3, Measure = src.Measure3 },
                        new IngredientItem { Name = src.Ingredient4, Measure = src.Measure4 },
                        new IngredientItem { Name = src.Ingredient5, Measure = src.Measure5 },
                        new IngredientItem { Name = src.Ingredient6, Measure = src.Measure6 },
                        new IngredientItem { Name = src.Ingredient7, Measure = src.Measure7 },
                        new IngredientItem { Name = src.Ingredient8, Measure = src.Measure8 },
                        new IngredientItem { Name = src.Ingredient9, Measure = src.Measure9 },
                        new IngredientItem { Name = src.Ingredient10, Measure = src.Measure10 },
                        new IngredientItem { Name = src.Ingredient11, Measure = src.Measure11 },
                        new IngredientItem { Name = src.Ingredient12, Measure = src.Measure12 },
                        new IngredientItem { Name = src.Ingredient13, Measure = src.Measure13 },
                        new IngredientItem { Name = src.Ingredient14, Measure = src.Measure14 },
                        new IngredientItem { Name = src.Ingredient15, Measure = src.Measure15 },
                        new IngredientItem { Name = src.Ingredient16, Measure = src.Measure16 },
                        new IngredientItem { Name = src.Ingredient17, Measure = src.Measure17 },
                        new IngredientItem { Name = src.Ingredient18, Measure = src.Measure18 },
                        new IngredientItem { Name = src.Ingredient19, Measure = src.Measure19 },
                        new IngredientItem { Name = src.Ingredient20, Measure = src.Measure20 }
                    }
                    .Where(i => !string.IsNullOrWhiteSpace(i.Name)).ToList()
                ));
        }
    }
}