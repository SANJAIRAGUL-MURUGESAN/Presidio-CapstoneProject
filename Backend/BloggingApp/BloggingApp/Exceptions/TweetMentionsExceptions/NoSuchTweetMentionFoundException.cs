namespace BloggingApp.Exceptions.TweetMentionsExceptions
{
    public class NoSuchTweetMentionFoundException:Exception
    {
        string msg;
        public NoSuchTweetMentionFoundException()
        {
            msg = "No Such Tweet Mentions Found!";
        }
        public override string Message => msg;
    }
}
