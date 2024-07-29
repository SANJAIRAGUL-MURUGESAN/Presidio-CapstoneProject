namespace BloggingApp.Exceptions.ReplyExceptions
{
    public class NoReplyFoundException : Exception
    {
        string msg;
        public NoReplyFoundException()
        {
            msg = "No Replies Found!";
        }
        public override string Message => msg;
    }
}
