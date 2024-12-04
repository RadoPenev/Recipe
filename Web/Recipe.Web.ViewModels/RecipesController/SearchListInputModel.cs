using System.Collections.Generic;

namespace Recipe.Web.ViewModels.RecipesController
{
    public class SearchListInputModel
    {
        public IEnumerable<int> Ingredients { get; set; }
    }
}
