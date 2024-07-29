using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class RetweetCommentLikes
    {
        [Key]
        public int Id { get; set; }
        public int LikedUserId { get; set; }
        // ForeignKey - Tweet ID
        public int RetweetCommentId { get; set; }
        public RetweetComment RetweetComment { get; set; }
    }
}
