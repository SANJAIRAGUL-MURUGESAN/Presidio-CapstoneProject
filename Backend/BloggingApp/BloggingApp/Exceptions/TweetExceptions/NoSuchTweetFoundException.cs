namespace BloggingApp.Exceptions.TweetExceptions
{
    public class NoSuchTweetFoundException :Exception
    {
        string msg;
        public NoSuchTweetFoundException()
        {
            msg = "No Such Tweet Found!";
        }
        public override string Message => msg;
    }
}
