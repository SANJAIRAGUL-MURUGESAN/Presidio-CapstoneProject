namespace BloggingApp.Models.UserDTOs
{
    public class TweetDetailsReturnDTO
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
        public int TweetLikesCount { get; set; }
        public string IsTweetLikedByUser { get; set; }
    }
}
