namespace BloggingApp.Exceptions.RetweetExceptions
{
    public class NoSuchRetweetFoundException : Exception
    {
        string msg;
        public NoSuchRetweetFoundException()
        {
            msg = "No Such Retweet Found!";
        }
        public override string Message => msg;
    }
}
