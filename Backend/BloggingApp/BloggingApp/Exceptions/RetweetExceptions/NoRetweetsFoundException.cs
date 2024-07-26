namespace BloggingApp.Exceptions.RetweetExceptions
{
    public class NoRetweetsFoundException: Exception
    {
        string msg;
        public NoRetweetsFoundException()
        {
            msg = "No Retweets Found!";
        }
        public override string Message => msg;
    }
}
