using Recipe.Data.Models;
using Recipe.Services.Mapping;

namespace Recipe.Web.ViewModels.Recipes
{
    public class IngredientViewModel:IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }
        public string Quantity { get; set; }
    }
}
