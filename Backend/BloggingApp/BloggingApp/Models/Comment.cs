using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
        public int UserId { get; set; }

        // ForeignKey - User ID
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public ICollection<Reply> CommentReplies { get; set; }//No effect on the table
        public ICollection<TweetCommentLikes> CommentLikes { get; set; }//No effect on the table

    }
}
