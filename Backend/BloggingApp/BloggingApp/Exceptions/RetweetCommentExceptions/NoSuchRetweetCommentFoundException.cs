namespace BloggingApp.Exceptions.RetweetCommentExceptions
{
    public class NoSuchRetweetCommentFoundException : Exception
    {
        string msg;
        public NoSuchRetweetCommentFoundException()
        {
            msg = "No Such Retweet Comment Found!";
        }
        public override string Message => msg;
    }
}
