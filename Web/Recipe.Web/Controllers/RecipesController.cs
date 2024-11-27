using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Data.Models;
using Recipe.Services.Data;
using Recipe.Web.ViewModels.Recipes;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recipe.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipeService recipeService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(ICategoriesService categoriesService,
            IRecipeService recipeService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.recipeService = recipeService;
            this.userManager = userManager;
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
            await this.recipeService.CreateAsync(input,user.Id);

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
