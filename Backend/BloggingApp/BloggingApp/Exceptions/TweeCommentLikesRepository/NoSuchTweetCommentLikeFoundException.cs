namespace BloggingApp.Exceptions.TweeCommentLikesRepository
{
    public class NoSuchTweetCommentLikeFoundException : Exception
    {
        string msg;
        public NoSuchTweetCommentLikeFoundException()
        {
            msg = "No Such TweetComment Like Found!";
        }
        public override string Message => msg;
    }
}
