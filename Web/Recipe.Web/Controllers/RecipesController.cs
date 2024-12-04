using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Recipe.Common;
using Recipe.Data.Models;
using Recipe.Services.Data;
using Recipe.Services.Messaging;
using Recipe.Web.ViewModels.Recipes;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recipe.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipeService recipeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IEmailSender emailSender;

        public RecipesController(ICategoriesService categoriesService,
            IRecipeService recipeService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IEmailSender emailSender)
        {
            this.categoriesService = categoriesService;
            this.recipeService = recipeService;
            this.userManager = userManager;
            this.environment = environment;
            this.emailSender = emailSender;
        }
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
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
                ModelState.AddModelError(string.Empty, ex.Message);
            }


            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
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

        public IActionResult ById(int id)
        {
            var recipe = this.recipeService.GetById<SingleRecipeViewModel>(id);

            return this.View(recipe);
        }

      
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel=this.recipeService.GetById<EditRecipeInputModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(inputModel);
        }


        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id,EditRecipeInputModel input)  
        {
            if (!this.ModelState.IsValid)
            {
                input.Id = id;
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }
            await this.recipeService.UpdateAsync(id,input);
            return RedirectToAction("ById",new {id});
        }
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
           await recipeService.DeleteAsync(id);
            return RedirectToAction("All");
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var recipe = this.recipeService.GetById<RecipesInListViewModel>(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{recipe.Name}</h1>");
            html.AppendLine($"<h3>{recipe.CategoryName}</h3>");
            html.AppendLine($"<img src=\"{recipe.ImageUrl}\" />");

                await this.emailSender.SendEmailAsync(
                    "radoUnikalen@gospod.com",
                    "Recipe",
                    "radomir.penev16@gmail.com",
                    recipe.Name,
                    html.ToString()
                );
            
            return this.RedirectToAction("ById", new { id });
        }


    }
}
