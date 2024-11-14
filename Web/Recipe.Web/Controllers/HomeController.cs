namespace Recipe.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Recipe.Data;
    using Recipe.Data.Common.Repositories;
    using Recipe.Data.Models;
    using Recipe.Services.Data;
    using Recipe.Web.ViewModels;
    using Recipe.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountsService;
        private readonly IMapper mapper;

        public HomeController(IGetCountsService getCountsService,IMapper mapper)
        {
            this.getCountsService = getCountsService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var counts = getCountsService.GetCount();
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
