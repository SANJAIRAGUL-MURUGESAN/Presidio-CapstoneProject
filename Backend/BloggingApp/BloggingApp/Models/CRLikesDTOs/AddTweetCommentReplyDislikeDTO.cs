namespace BloggingApp.Models.CRLikesDTOs
{
    public class AddTweetCommentReplyDislikeDTO
    {
        public int LikedUserId { get; set; }
        // ForeignKey - Tweet ID
        public int ReplyId { get; set; }
    }
}
