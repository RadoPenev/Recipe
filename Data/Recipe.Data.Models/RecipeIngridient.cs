using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Data.Models
{
    public class RecipeIngridient
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public int IngridientId { get; set; }

        public virtual Ingredient Ingridient { get; set; }
    }
}
