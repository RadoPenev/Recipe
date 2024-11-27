using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Data.Models;
using Recipe.Services.Data;
using Recipe.Web.ViewModels.Recipes;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recipe.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipeService recipeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public RecipesController(ICategoriesService categoriesService,
            IRecipeService recipeService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.recipeService = recipeService;
            this.userManager = userManager;
            this.environment = environment;
        }
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems=this.categoriesService.GetAllAsKeyValuePairs();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View();
            }
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.recipeService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
ModelState.AddModelError(string.Empty,ex.Message);
            }
           

            return this.Redirect("/");
        }

        public IActionResult All(int id=1)
        {
            const int ItemsPerPage = 12;
            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Recipes = this.recipeService.GetAll(id),
                RecipesCount = this.recipeService.GetCount(),
            };
            return this.View(viewModel);
        }
    }
}
