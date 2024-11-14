using Recipe.Web.Models;
using Recipe.Web.ViewModels.Home;
namespace Recipe.Services.Data
{
    public interface IGetCountsService
    {
        CountsDto GetCount();
    }
}
