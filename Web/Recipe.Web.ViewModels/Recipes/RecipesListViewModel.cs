using Recipe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web.ViewModels.Recipes
{
    public class RecipesListViewModel
    {
        public IEnumerable<RecipesInListViewModel> Recipes { get; set; }
        public bool HasPreviousPage => this.PageNumber > 1;
        public bool HasNextPage => this.PageNumber < this.PagesCount;
        public int PreviousPageNumber => this.PageNumber - 1;
        public int NextPageNumber => this.PageNumber + 1;
        public int PageNumber { get; set; }
        public int PagesCount => (int)Math.Ceiling((double)this.RecipesCount / this.ItemsPerPage);
        public int RecipesCount { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
