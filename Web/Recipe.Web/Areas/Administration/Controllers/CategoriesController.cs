using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipe.Data;
using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;

namespace Recipe.Web.Areas.Administration.Controllers
{
    public class CategoriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Category> dataRepository;

        public CategoriesController(IDeletableEntityRepository<Category> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Categories
        public async Task<IActionResult> Index()
        {
              return this.View(await this.dataRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return NotFound();
            }

            var category =  this.dataRepository.All()          
                .FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Administration/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(category);
                await this.dataRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Administration/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return NotFound();
            }

            var category = this.dataRepository.All().FirstOrDefault(x=>x.Id==id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Administration/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(category);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Administration/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return NotFound();
            }

            var category = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Administration/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.dataRepository.All() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = this.dataRepository.All().FirstOrDefault(x=>x.Id==id);
            if (category != null)
            {
                this.dataRepository.Delete(category);
            }
            
            await this.dataRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
