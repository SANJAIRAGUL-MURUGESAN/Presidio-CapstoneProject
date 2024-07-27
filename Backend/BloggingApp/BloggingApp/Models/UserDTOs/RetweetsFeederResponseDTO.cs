namespace BloggingApp.Models.UserDTOs
{
    public class RetweetsFeederResponseDTO
    {
        public int TweetId { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
        public string IsCommentEnable { get; set; }
        public int UserId { get; set; }
        public string TweetFile1 { get; set; }
        public string TweetFile2 { get; set; }
        public string TweetOwnerUserName { get; set; }
        public string TweetOwnerUserId { get; set; }
        //public string TweetOwnerId { get; set; }
        public string TweetOwnerProfileImgLink { get; set; }

        // Retweet Details
        public int RetweetId { get; set; }
        public string RetweetContent { get; set; }
        public DateTime RetweetDateTime { get; set; }
        public string IsRetweetCommentEnable { get; set; }
        public string RetweetUserName { get; set; }
        public string RetweetUserId { get; set; }
        public string RetweetUserProfileImgLink { get; set; }
        public int RetweetLikesCount { get; set; }
        public string IsRetweetLikedByUser { get; set; }

    }
}
