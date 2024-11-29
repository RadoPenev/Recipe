using AutoMapper;
using Recipe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web.ViewModels.Recipes
{
    public class EditRecipeInputModel : BaseRecipeInputModel, IMapFrom<Recipe.Data.Models.Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe.Data.Models.Recipe, EditRecipeInputModel>()
                .ForMember(x => x.CookingTime, opt => opt.MapFrom(x=>(int)x.CookingTime.TotalMinutes))
                 .ForMember(x => x.PreparationTime, opt => opt.MapFrom(x => (int)x.PreparationTime.TotalMinutes));
        }
    }
}
