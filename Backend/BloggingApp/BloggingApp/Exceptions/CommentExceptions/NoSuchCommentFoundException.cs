namespace BloggingApp.Exceptions.CommentExceptions
{
    public class NoSuchCommentFoundException : Exception
    {
        string msg;
        public NoSuchCommentFoundException()
        {
            msg = "No Such Comment Found!";
        }
        public override string Message => msg;
    }
}
