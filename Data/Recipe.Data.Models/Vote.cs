using Microsoft.AspNetCore.Components.Server;
using Recipe.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Data.Models
{
    public class Vote:BaseModel<int>
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public byte Value { get; set; }
    }
}
