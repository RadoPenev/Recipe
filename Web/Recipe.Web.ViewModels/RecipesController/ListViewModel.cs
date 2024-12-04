using Recipe.Web.ViewModels.Recipes;
using System.Collections.Generic;

namespace Recipe.Web.ViewModels.RecipesController
{
    public class ListViewModel
    {
        public IEnumerable<RecipesInListViewModel> Recipes { get; set; } 
    }
}
