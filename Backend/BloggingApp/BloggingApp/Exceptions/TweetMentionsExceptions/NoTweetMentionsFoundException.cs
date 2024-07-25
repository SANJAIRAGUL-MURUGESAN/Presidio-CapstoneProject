namespace BloggingApp.Exceptions.TweetMentionsExceptions
{
    public class NoTweetMentionsFoundException: Exception
    {
        string msg;
        public NoTweetMentionsFoundException()
        {
            msg = "No Tweet Mentions Found!";
        }
        public override string Message => msg;
    }
}
