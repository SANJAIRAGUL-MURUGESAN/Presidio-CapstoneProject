namespace BloggingApp.Models.CRLikesDTOs
{
    public class AddTweetCommentReplyLikeDTO
    {
        public int LikedUserId { get; set; }
        // ForeignKey - Tweet ID
        public int ReplyId { get; set; }
    }
}
