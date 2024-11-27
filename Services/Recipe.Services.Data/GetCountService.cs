using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using Recipe.Web.Models;
using System.Linq;

namespace Recipe.Services.Data
{
    public class GetCountService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe.Data.Models.Recipe> recipesRepository;

        public GetCountService(IDeletableEntityRepository<Category> categoriesRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe.Data.Models.Recipe> recipesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.imagesRepository = imagesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipesRepository = recipesRepository;
        }
        public CountsDto GetCount()
        {
            var data = new CountsDto()
            {
                RecipiesCount = this.recipesRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
                IngredientsCount = this.ingredientsRepository.All().Count(),
                ImagesCount = this.imagesRepository.All().Count(),
            };

            return data;
        }

        
    }
}
