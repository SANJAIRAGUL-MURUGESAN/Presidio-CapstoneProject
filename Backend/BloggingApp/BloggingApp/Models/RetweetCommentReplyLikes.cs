using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class RetweetCommentReplyLikes
    {
        [Key]
        public int Id { get; set; }
        public int LikedUserId { get; set; }
        // ForeignKey - Tweet ID
        public int ReplyCommentReplyId { get; set; }
        public RetweetCommentReply RetweetCommentReply { get; set; }
    }
}
