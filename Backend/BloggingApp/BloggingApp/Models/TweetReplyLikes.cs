using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class TweetReplyLikes
    {
        [Key]
        public int Id { get; set; }
        public int LikedUserId { get; set; }
        // ForeignKey - Tweet ID
        public int ReplyId { get; set; }
        public Reply Reply { get; set; }

    }
}
