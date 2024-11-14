using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipe.Data.Models;
namespace Recipe.Data.Seeding
{
    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category {Name="Pizza"});
            await dbContext.Categories.AddAsync(new Category { Name = "Tart" });
            await dbContext.Categories.AddAsync(new Category { Name = "Burger" });
            await dbContext.Categories.AddAsync(new Category { Name = "Manja" });

            await dbContext.SaveChangesAsync();
        }
    }
}
