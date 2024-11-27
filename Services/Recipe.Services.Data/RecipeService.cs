using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using Recipe.Services.Mapping;
using Recipe.Web.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Recipe.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly IDeletableEntityRepository<Recipe.Data.Models.Recipe> recipeRepo;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepo;
        private readonly IRepository<Image> imageRepo;

        public RecipeService(IDeletableEntityRepository<Recipe.Data.Models.Recipe> recipeRepo,
           IDeletableEntityRepository<Ingredient>  ingredientRepo,
           IRepository<Image> imageRepo)
        {
            this.recipeRepo = recipeRepo;
            this.ingredientRepo = ingredientRepo;
            this.imageRepo = imageRepo;
        }

        public async Task CreateAsync(CreateRecipeInputModel input,string userId)
        {
            var recipe = new Recipe.Data.Models.Recipe { 
                AddedByUserId = userId,
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

        public IEnumerable<RecipesInListVIewModel> GetAll(int page, int itemsPerPage = 12)
        {
           return this.recipeRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<RecipesInListVIewModel>()
                .ToList();
        }

        public int GetCount()
        {
            return this.recipeRepo.All().Count();
        }
    }
}
