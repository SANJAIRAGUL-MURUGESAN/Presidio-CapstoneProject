namespace BloggingApp.Exceptions.TweetExceptions
{
    public class NoTweetsFoundException : Exception
    {
        string msg;
        public NoTweetsFoundException()
        {
            msg = "No Tweets Found!";
        }
        public override string Message => msg;
    }
}
