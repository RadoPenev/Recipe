using System.Collections.Generic;

namespace Recipe.Web.ViewModels.RecipesController
{
    public class SearchIndexViewModel
    {
        public IEnumerable<IngredientNameIdViewModel> Ingredients { get; set; }
    }
}
