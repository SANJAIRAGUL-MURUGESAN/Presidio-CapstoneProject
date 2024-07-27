namespace BloggingApp.Exceptions.TweetLikesRepository
{
    public class NoTweetLikesFoundException: Exception
    {
        string msg;
        public NoTweetLikesFoundException()
        {
            msg = "No Tweet Likes Found!";
        }
        public override string Message => msg;
    }
}
