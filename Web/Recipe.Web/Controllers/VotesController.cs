using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipe.Services.Data;
using Recipe.Web.ViewModels.Votes;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recipe.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel input)
        {
            var userId=this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.RecipeId,userId,input.Value);
            var averageVotes = this.votesService.GetAverageVote(input.RecipeId);
            return new PostVoteResponseModel { AverageVote = averageVotes};
        }
    }
}
