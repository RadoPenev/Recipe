using Microsoft.AspNetCore.Mvc;
using Recipe.Services.Data;
using Recipe.Web.ViewModels.Recipes;
using System.Threading.Tasks;

namespace Recipe.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipeService recipeService;

        public RecipesController(ICategoriesService categoriesService,
            IRecipeService recipeService)
        {
            this.categoriesService = categoriesService;
            this.recipeService = recipeService;
        }
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems=this.categoriesService.GetAllAsKeyValuePairs();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View();
            }

            await this.recipeService.CreateAsync(input);

            return this.Redirect("/");
        }


    }
}
