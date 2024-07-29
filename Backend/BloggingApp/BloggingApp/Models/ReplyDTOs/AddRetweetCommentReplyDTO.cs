namespace BloggingApp.Models.ReplyDTOs
{
    public class AddRetweetCommentReplyDTO
    {
        public string ReplyContent { get; set; }
        public int UserId { get; set; }

        // ForeignKey - Comment ID
        public int Comment_ReplyId { get; set; }
    }
}
