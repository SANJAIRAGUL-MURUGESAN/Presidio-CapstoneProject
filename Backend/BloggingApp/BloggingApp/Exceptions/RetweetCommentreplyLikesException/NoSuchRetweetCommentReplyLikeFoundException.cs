namespace BloggingApp.Exceptions.RetweetCommentreplyLikesException
{
    public class NoSuchRetweetCommentReplyLikeFoundException : Exception
    {

        string msg;
        public NoSuchRetweetCommentReplyLikeFoundException()
        {
            msg = "No Retweet Comment Reply Like Found!";
        }
        public override string Message => msg;
    }
}
