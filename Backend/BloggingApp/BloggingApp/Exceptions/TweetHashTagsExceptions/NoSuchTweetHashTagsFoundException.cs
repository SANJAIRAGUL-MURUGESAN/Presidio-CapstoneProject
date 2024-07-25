namespace BloggingApp.Exceptions.TweetHashTagsExceptions
{
    public class NoSuchTweetHashTagsFoundException : Exception
    {
        string msg;
        public NoSuchTweetHashTagsFoundException()
        {
            msg = "No Such Tweet HashTags Found!";
        }
        public override string Message => msg;
    }
}
