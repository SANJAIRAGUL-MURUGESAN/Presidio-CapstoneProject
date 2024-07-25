namespace BloggingApp.Exceptions.TweetFilesExceptions
{
    public class NoTweetFilesException : Exception
    {
        string msg;
        public NoTweetFilesException()
        {
            msg = "No Tweet Files Found!";
        }
        public override string Message => msg;
    }
}
