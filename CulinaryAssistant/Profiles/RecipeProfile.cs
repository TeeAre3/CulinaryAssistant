using AutoMapper;
using CulinaryAssistant.Models;

namespace CulinaryAssistant.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<MealDetail, CleanRecipe>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.ExtractIngredients()));
        }
    }
}