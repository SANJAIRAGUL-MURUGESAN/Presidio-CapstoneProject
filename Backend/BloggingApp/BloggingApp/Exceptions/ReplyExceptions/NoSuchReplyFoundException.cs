namespace BloggingApp.Exceptions.ReplyExceptions
{
    public class NoSuchReplyFoundException : Exception
    {
        string msg;
        public NoSuchReplyFoundException()
        {
            msg = "No Such Reply Found!";
        }
        public override string Message => msg;
    }
}
