using AutoMapper;
using Recipe.Services.Mapping;
using System.Linq;

namespace Recipe.Web.ViewModels.Recipes
{
    public class RecipesInListViewModel : IMapFrom<Recipe.Data.Models.Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe.Data.Models.Recipe, RecipesInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => x.Images.FirstOrDefault().RemoteImageUrl != null ?
                    x.Images.FirstOrDefault().RemoteImageUrl :
                    "/images/recipes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
