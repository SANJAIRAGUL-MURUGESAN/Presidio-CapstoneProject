using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class TweetCommentLikes
    {
        [Key]
        public int Id { get; set; }
        public int LikedUserId { get; set; }
        // ForeignKey - Tweet ID
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
