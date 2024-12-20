﻿using System.Collections;
using System.Collections.Generic;

namespace Recipe.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexPageRecipeViewModel> RandomRecipes { get; set; }
        public int RecipiesCount { get; set; }

        public int CategoriesCount { get; set; }

        public int IngredientsCount { get; set; }

        public int ImagesCount { get; set; }
    }
}
