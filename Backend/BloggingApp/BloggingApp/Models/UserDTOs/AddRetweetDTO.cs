namespace BloggingApp.Models.UserDTOs
{
    public class AddRetweetDTO
    {
        public string RetweetContent { get; set; }
        public string IsCommentEnable { get; set; }
        public int ActualTweetId { get; set; }
        public int UserId { get; set; }
        public List<string> RetweetMentions { get; set; }
        public List<string> RetweetHashTags { get; set; }
    }
}
