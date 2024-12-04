using Recipe.Data.Models;
using Recipe.Services.Mapping;

namespace Recipe.Web.ViewModels.RecipesController
{
    public class IngredientNameIdViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
