namespace BloggingApp.Exceptions.RetweetCommentreplyLikesException
{
    public class NoRetweetCommentReplyLikesFoundException : Exception
    {

        string msg;
        public NoRetweetCommentReplyLikesFoundException()
        {
            msg = "No Retweet Comment Reply Likes Found!";
        }
        public override string Message => msg;
    }
}
