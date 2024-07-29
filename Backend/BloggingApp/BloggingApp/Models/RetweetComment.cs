using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class RetweetComment
    {
        [Key]
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
        public int UserId { get; set; }

        // ForeignKey - User ID
        public int RetweetId { get; set; }
        public Retweet Retweet { get; set; }

        public ICollection<RetweetCommentReply> RetweetCommentReplies { get; set; }//No effect on the table
        public ICollection<RetweetCommentLikes> RetweetCommentLikes { get; set; }//No effect on the table
    }
}
