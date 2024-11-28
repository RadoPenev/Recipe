using System.Threading.Tasks;

namespace Recipe.Services.Data
{
    public interface IVotesService
    {
        Task SetVoteAsync(int recipeId, string userId, byte value);

        double GetAverageVote(int recipeId);
    }
}
