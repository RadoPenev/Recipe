using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using Recipe.Services.Mapping;
using Recipe.Web.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.IO;
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

        

        public async Task CreateAsync(CreateRecipeInputModel input,string userId, string imagePath)
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

            Directory.CreateDirectory($"{imagePath}/recipes/");

            foreach (var item in input.Ingredients)
            {
                var ingredient=this.ingredientRepo.All().FirstOrDefault(x=>x.Name==item.IngredientName);
                if (ingredient==null)
                {
                    ingredient = new Ingredient { Name = item.IngredientName };
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                   Ingredient= ingredient,
                    Quantity = item.Quantity
                });
            }

            var allowedExtensions = new[] { "jpeg", "png", "gif" };

            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!allowedExtensions.Any(x => x.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}!");
                }
                var dbImages = new Image
                {
                    AddedByUserId=userId,
                    Extension=extension,
                };
                recipe.Images.Add(dbImages);

              
                var physicalPath = $"{imagePath}/recipes/{dbImages.Id}.{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

            }


            await this.recipeRepo.AddAsync(recipe);
            await this.recipeRepo.SaveChangesAsync();
        }

        public IEnumerable<RecipesInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
           return this.recipeRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<RecipesInListViewModel>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.recipeRepo.AllAsNoTracking().Where(x=>x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.recipeRepo.All().Count();
        }
    }
}
