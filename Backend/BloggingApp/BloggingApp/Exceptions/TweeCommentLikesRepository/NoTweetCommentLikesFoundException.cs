namespace BloggingApp.Exceptions.TweeCommentLikesRepository
{
    public class NoTweetCommentLikesFoundException : Exception
    {
        string msg;
        public NoTweetCommentLikesFoundException()
        {
            msg = "No TweetComment Likes Found!";
        }
        public override string Message => msg;
    }
}
