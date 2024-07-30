namespace BloggingApp.Exceptions.RetweetCommentLikesExceptions
{
    public class NoSuchRetweetCommentLikeFoundException: Exception
    {
        string msg;
        public NoSuchRetweetCommentLikeFoundException()
        {
            msg = "No Such Retweet Comment Like Found!";
        }
        public override string Message => msg;
    }
}
