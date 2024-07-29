namespace BloggingApp.Models.CommentDTOs
{
    public class AddCommentDTO
    {
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public int TweetId { get; set; }
    }
}
