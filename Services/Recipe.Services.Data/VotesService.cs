﻿using Recipe.Data.Common.Repositories;
using Recipe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Services.Data
{
    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVote(int recipeId)
        {
            return this.votesRepository.All().Where(x => x.RecipeId == recipeId).Average(x=>x.Value);
        }

        public async Task SetVoteAsync(int recipeId, string userId, byte value)
        {
            var vote=this.votesRepository.All()
                .FirstOrDefault(x=> x.RecipeId==recipeId && x.UserId==userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    RecipeId = recipeId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
