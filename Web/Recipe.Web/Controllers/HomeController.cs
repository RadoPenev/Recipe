using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Recipe.Services.Data;
using Recipe.Web.ViewModels;
using Recipe.Web.ViewModels.Home;

namespace Recipe.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountsService;
        private readonly IRecipeService recipeService;

        public HomeController(IGetCountsService getCountsService,
            IRecipeService recipeService)
        {
            this.getCountsService = getCountsService;
            this.recipeService = recipeService;
        }

        public IActionResult Index()
        {
            var counts = this.getCountsService.GetCount();
            //var viewModel = this.mapper.Map<IndexViewModel>(counts);

            var viewModel = new IndexViewModel
            {
                RecipiesCount = counts.RecipiesCount,
                IngredientsCount = counts.IngredientsCount,
                ImagesCount = counts.ImagesCount,
                CategoriesCount = counts.CategoriesCount,
                RandomRecipes = this.recipeService.GetRandom<IndexPageRecipeViewModel>(3),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
