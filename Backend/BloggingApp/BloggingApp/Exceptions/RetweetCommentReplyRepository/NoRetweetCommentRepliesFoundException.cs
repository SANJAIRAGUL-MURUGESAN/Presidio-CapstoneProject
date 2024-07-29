namespace BloggingApp.Exceptions.RetweetCommentReplyRepository
{
    public class NoRetweetCommentRepliesFoundException : Exception
    {
        string msg;
        public NoRetweetCommentRepliesFoundException()
        {
            msg = "No Retweet Comment Replies Found!";
        }
        public override string Message => msg;
    }
}
