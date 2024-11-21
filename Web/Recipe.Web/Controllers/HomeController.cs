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
       

        public HomeController(IGetCountsService getCountsService)
        {
            this.getCountsService = getCountsService;
        }

        public IActionResult Index()
        {
            var counts = this.getCountsService.GetCount();
            //var viewModel = this.mapper.Map<IndexViewModel>(counts);

            var viewModel = new IndexViewModel
            {
                RecipiesCount = counts.RecipiesCount,
                IngridientsCount = counts.IngridientsCount,
                ImagesCount = counts.ImagesCount,
                CategoriesCount = counts.CategoriesCount,
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
