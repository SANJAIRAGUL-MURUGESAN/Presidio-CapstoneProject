namespace BloggingApp.Exceptions.TweetCommentReplyLikesRepository
{
    public class NoTweetCommentReplyLikesFoundException : Exception
    {
        string msg;
        public NoTweetCommentReplyLikesFoundException()
        {
            msg = "No TweetComment Reply Likes Found!";
        }
        public override string Message => msg;
    }
}
