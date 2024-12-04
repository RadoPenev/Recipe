using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using Recipe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipe.Services.Data.Models
{
    public class IngredientService : IIngredientService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public IngredientService(IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }
        public IEnumerable<T> GetAllPopular<T>()
        {
            return this.ingredientRepository.All().OrderByDescending(x=>x.Recipes.Count()).To<T>().ToList();
        }
    }
}
