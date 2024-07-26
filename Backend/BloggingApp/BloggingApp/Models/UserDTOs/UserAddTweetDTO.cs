namespace BloggingApp.Models.UserDTOs
{
    public class UserAddTweetDTO
    {
        public List<IFormFile> Images { get; set; }
        public int TweetId { get; set; }
    }
}
