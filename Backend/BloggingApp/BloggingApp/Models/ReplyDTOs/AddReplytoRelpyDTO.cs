namespace BloggingApp.Models.ReplyDTOs
{
    public class AddReplytoRelpyDTO
    {
        public string ReplyContent { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public int ReplyId { get; set; }
    }
}
