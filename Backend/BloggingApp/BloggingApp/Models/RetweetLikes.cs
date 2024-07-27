namespace BloggingApp.Models
{
    public class RetweetLikes
    {
        public int Id { get; set; }
        public int LikedUserId { get; set; }

        // ForeignKey - Tweet ID
        public int RetweetId { get; set; }
        public Retweet Retweet { get; set; }
    }
}
