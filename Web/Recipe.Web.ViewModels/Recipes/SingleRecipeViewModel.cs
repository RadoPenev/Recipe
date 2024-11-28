using AutoMapper;
using Recipe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipe.Web.ViewModels.Recipes
{
    public class SingleRecipeViewModel : IMapFrom<Recipe.Data.Models.Recipe>,IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AddedByUserUserName { get; set; }
        public string ImageUrl { get; set; }
        public string Instructions { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int PortionsCount { get; set; }
        public  double AverageVote { get; set; }
        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe.Data.Models.Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.AverageVote, otp => otp.MapFrom(x => x.Votes.Count==0 ? 0: x.Votes.Average(y => y.Value)))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Images.FirstOrDefault().RemoteImageUrl ?? "/images/recipes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }


    }
}
