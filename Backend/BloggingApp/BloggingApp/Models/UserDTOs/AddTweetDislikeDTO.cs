namespace BloggingApp.Models.UserDTOs
{
    public class AddTweetDislikeDTO
    {
        public int TweetId { get; set; }
        public int LikedUserId { get; set; }
    }
}
