namespace BloggingApp.Models
{
    public class TweetLikes
    {
        public int Id { get; set; }
        public int LikedUserId { get; set; }

        // ForeignKey - Tweet ID
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }
    }
}
