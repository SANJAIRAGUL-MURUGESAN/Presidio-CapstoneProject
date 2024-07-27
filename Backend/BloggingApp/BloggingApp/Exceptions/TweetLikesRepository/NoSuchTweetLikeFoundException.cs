namespace BloggingApp.Exceptions.TweetLikesRepository
{
    public class NoSuchTweetLikeFoundException : Exception
    {
        string msg;
        public NoSuchTweetLikeFoundException()
        {
            msg = "No Such Tweet Like Found!";
        }
        public override string Message => msg;
    }
}
