namespace Recipe.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Category : BaseDeletableModel<int>
    {

        public Category()
        {
            this.Recipes=new HashSet<Recipe>();
        }

        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
