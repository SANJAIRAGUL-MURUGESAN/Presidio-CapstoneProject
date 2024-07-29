using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class RetweetCommentReply
    {
        [Key]
        public int Id { get; set; }
        public string ReplyType { get; set; }
        public string ReplyContent { get; set; }
        public int UserId { get; set; }
        public DateTime ReplyDateTime { get; set; }

        // ForeignKey - Comment ID
        public int RetweetCommentId { get; set; }
        public RetweetComment RetweetComment { get; set; }
        public int ReplyId { get; set; }
        public ICollection<RetweetCommentReplyLikes> RetweetCommentReplyLikes { get; set; }//No effect on the table
    }
}
