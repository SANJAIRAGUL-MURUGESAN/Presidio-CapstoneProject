using BloggingApp.Models.ReplyDTOs;

namespace BloggingApp.Models.CommentDTOs
{
    public class RetweetCommentDetailsReturnDTO
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PUserId { get; set; }

        public string UserProfileLink { get; set; }

        // ForeignKey - User ID
        public int TweetId { get; set; }
        public List<RetweetReplyDetailsReturnDTO> Replies { get; set; }
        public int LikesCount { get; set; }
        public string IsLikedByUser { get; set; }
    }
}
