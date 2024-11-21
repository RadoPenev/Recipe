using Recipe.Data.Common.Models;
using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }).ToList().Select(x=> new KeyValuePair<string,string>(x.Id.ToString(),x.Name.ToString())) ;
        }
    }
}
