namespace BloggingApp.Exceptions.RetweetCommentLikesExceptions
{
    public class NoRetweetCommentLikesFoundException : Exception
    {
        string msg;
        public NoRetweetCommentLikesFoundException()
        {
            msg = "No Retweet Comment Likes Found!";
        }
        public override string Message => msg;
    }
}
