namespace BloggingApp.Exceptions.RetweetLikesExceptions
{
    public class NoSuchRetweetLikesFoundException : Exception
    {
        string msg;
        public NoSuchRetweetLikesFoundException()
        {
            msg = "No Such Retweet Like Found!";
        }
        public override string Message => msg;
    }
}
