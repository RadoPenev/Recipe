﻿using System.ComponentModel.DataAnnotations;

namespace Recipe.Web.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        public int RecipeId { get; set; }

        [Range(1,5)]
        public byte Value { get; set; }
    }
}
