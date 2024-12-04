using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Services.Data
{
    public interface IIngredientService
    {
        IEnumerable<T> GetAllPopular<T>();
    }
}
