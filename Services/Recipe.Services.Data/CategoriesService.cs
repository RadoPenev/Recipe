using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Recipe.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository) 
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
           return this.categoriesRepository.All().Select(x=> new
            {
                x.Id, x.Name
            }).ToList()
            .OrderBy(x=>x.Name)
            .Select(x=> new KeyValuePair<string,string>(x.Id.ToString(),x.Name.ToString())) ;
        }
    }
}
