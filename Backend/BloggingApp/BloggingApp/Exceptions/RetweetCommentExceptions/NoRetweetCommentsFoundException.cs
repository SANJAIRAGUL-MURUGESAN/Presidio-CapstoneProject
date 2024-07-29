namespace BloggingApp.Exceptions.RetweetCommentExceptions
{
    public class NoRetweetCommentsFoundException : Exception
    {
        string msg;
        public NoRetweetCommentsFoundException()
        {
            msg = "No Retweet Comments Found!";
        }
        public override string Message => msg;
    }
}
