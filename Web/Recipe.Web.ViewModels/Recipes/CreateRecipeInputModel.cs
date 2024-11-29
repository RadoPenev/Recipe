using Microsoft.AspNetCore.Http;
using Recipe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipe.Web.ViewModels.Recipes
{
    public class CreateRecipeInputModel : BaseRecipeInputModel,IMapFrom<Recipe.Data.Models.Recipe>
    {
  
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }

    }
}
