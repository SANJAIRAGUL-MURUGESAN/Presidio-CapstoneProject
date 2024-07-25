namespace BloggingApp.Exceptions.TweetHashTagsExceptions
{
    public class NoTweetHashTagsFoundException :Exception
    {
        string msg;
        public NoTweetHashTagsFoundException()
        {
            msg = "No Such Tweet HashTags Found!";
        }
        public override string Message => msg;
    }
}
