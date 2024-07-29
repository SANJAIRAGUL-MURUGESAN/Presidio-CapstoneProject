using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }
        public string ReplyType { get; set; }
        public string ReplyContent { get; set; }
        public int UserId { get; set; }
        public DateTime ReplyDateTime { get; set; }

        // ForeignKey - Comment ID
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int ReplyId { get; set; }

        public ICollection<TweetReplyLikes> ReplyLikes { get; set; }//No effect on the table
    }
}
