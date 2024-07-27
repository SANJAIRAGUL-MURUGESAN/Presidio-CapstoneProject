namespace BloggingApp.Exceptions.RetweetLikesExceptions
{
    public class NoRetweetLikesFoundException: Exception
    {
        string msg;
        public NoRetweetLikesFoundException()
        {
            msg = "No Retweet Likes Found!";
        }
        public override string Message => msg;
    }
}
