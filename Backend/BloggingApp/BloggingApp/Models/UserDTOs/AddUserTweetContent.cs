namespace BloggingApp.Models.UserDTOs
{
    public class AddUserTweetContent
    {
        public int UserId { get; set; }
        public string TweetContent { get; set; }
        public string IsCommentEnable { get; set; }
        public List<string> TweetHashtags { get; set; }
        public List<string> TweetMentions { get; set; }
    }
}
