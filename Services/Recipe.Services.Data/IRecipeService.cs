using Recipe.Web.ViewModels.Recipes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Services.Data
{
    public interface IRecipeService
    {
        Task CreateAsync(CreateRecipeInputModel input,string userId, string imagePath);

        IEnumerable<RecipesInListViewModel> GetAll(int page, int itemsPerPage=12);

        int GetCount();

        T GetById<T>(int id);
    }
}
