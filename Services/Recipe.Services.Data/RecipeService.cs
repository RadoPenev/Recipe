using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using Recipe.Web.ViewModels.Recipes;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Recipe.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly IDeletableEntityRepository<Recipe.Data.Models.Recipe> recipeRepo;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepo;

        public RecipeService(IDeletableEntityRepository<Recipe.Data.Models.Recipe> recipeRepo,
           IDeletableEntityRepository<Ingredient>  ingredientRepo)
        {
            this.recipeRepo = recipeRepo;
            this.ingredientRepo = ingredientRepo;
        }

        public async Task CreateAsync(CreateRecipeInputModel input)
        {
            var recipe = new Recipe.Data.Models.Recipe { 
            CategoryId = input.CategoryId,
            Name = input.Name,
            CookingTime=TimeSpan.FromMinutes(input.CookingTime),
            PreparationTime= TimeSpan.FromMinutes(input.PreparationTime),
            PortionsCount=input.PortionsCount,
            Instructions=input.Instructions,
            };

            foreach (var item in input.Ingredients)
            {
                var ingredient=this.ingredientRepo.All().FirstOrDefault(x=>x.Name==item.IngredientName);
                if (ingredient==null)
                {
                    ingredient = new Ingredient { Name = item.IngredientName };
                }

                recipe.Ingridients.Add(new RecipeIngridient
                {
                   Ingridient= ingredient,
                    Quantity = item.Quantity
                });
                
            }
            await this.recipeRepo.AddAsync(recipe);
            await this.recipeRepo.SaveChangesAsync();

        }
    }
}
