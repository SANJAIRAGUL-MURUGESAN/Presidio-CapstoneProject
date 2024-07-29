namespace BloggingApp.Exceptions.RetweetCommentReplyRepository
{
    public class NoSuchRetweetCommentReplyFoundException : Exception
    {
        string msg;
        public NoSuchRetweetCommentReplyFoundException()
        {
            msg = "No such Retweet Comment Reply Found!";
        }
        public override string Message => msg;
    }
}
