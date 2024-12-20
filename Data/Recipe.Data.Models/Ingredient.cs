﻿namespace Recipe.Data.Models
{
    using System.Collections.Generic;
    using Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {

        public Ingredient()
        {
            this.Recipes = new HashSet<RecipeIngredient>();
        }

        public string Name { get; set; }

        public ICollection<RecipeIngredient> Recipes { get; set; }
    }
}
