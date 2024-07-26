﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BloggingApp.Models.UserDTOs
{
    public class TweetsFeederDTO
    {
        public int TweetId { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
        public string IsCommentEnable { get; set; }
        public int RepostTweetId { get; set; }
        public int UserId { get; set; }
        public string TweetFile1 { get; set; }
        public string TweetFile2 { get; set; }
        public string TweetOwnerUserName { get; set; }
        public string TweetOwnerUserId { get; set; }
        //public string TweetOwnerId { get; set; }
        public string TweetOwnerProfileImgLink { get; set; }
    }
}
