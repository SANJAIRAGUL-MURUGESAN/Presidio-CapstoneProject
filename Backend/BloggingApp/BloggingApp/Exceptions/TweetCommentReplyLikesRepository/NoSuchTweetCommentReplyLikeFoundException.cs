namespace BloggingApp.Exceptions.TweetCommentReplyLikesRepository
{
    public class NoSuchTweetCommentReplyLikeFoundException : Exception
    {
        string msg;
        public NoSuchTweetCommentReplyLikeFoundException()
        {
            msg = "No Such TweetComment Reply Like Found!";
        }
        public override string Message => msg;
    }
}
