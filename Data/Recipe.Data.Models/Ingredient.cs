namespace Recipe.Data.Models
{
    using System.Collections.Generic;
    using Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {

        public Ingredient()
        {
            this.Recipes = new HashSet<RecipeIngridient>();
        }

        public string Name { get; set; }

        public ICollection<RecipeIngridient> Recipes { get; set; }
    }
}
