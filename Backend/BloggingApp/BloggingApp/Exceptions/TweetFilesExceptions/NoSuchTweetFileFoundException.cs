namespace BloggingApp.Exceptions.TweetFilesExceptions
{
    public class NoSuchTweetFileFoundException : Exception
    {
        string msg;
        public NoSuchTweetFileFoundException()
        {
            msg = "No Such Tweet File Found!";
        }
        public override string Message => msg;
    }
}
