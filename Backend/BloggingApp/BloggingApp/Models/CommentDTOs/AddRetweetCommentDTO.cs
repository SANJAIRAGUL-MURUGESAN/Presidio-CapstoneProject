namespace BloggingApp.Models.CommentDTOs
{
    public class AddRetweetCommentDTO
    {
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public int RetweetId { get; set; }
    }
}
