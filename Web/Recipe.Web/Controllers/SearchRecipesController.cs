using Microsoft.AspNetCore.Mvc;
using Recipe.Services.Data;
using Recipe.Web.ViewModels.Recipes;
using Recipe.Web.ViewModels.RecipesController;
namespace Recipe.Web.Controllers
{
    public class SearchRecipesController : BaseController
    {
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientService;

        public SearchRecipesController(IRecipeService recipeService,IIngredientService ingredientService)
        {
            this.recipeService = recipeService;
            this.ingredientService = ingredientService;
        }
        public IActionResult Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Ingredients = this.ingredientService.GetAllPopular<IngredientNameIdViewModel>(),
            };


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Recipes = this.recipeService.GetByIngredient<RecipesInListViewModel>(input.Ingredients),
            };
            return this.View(viewModel);
        }
    }
}
