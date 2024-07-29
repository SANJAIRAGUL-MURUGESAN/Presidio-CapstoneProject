namespace BloggingApp.Models.ReplyDTOs
{
    public class AddRetweetCommentReplytoRelpy
    {
        public string ReplyContent { get; set; }
        public int UserId { get; set; }
        public int RetweetCommentId { get; set; }
        public int ReweetCommentReplyId { get; set; }
    }
}
