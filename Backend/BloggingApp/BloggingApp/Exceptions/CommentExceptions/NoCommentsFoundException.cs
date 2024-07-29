namespace BloggingApp.Exceptions.CommentExceptions
{
    public class NoCommentsFoundException :Exception
    {
        string msg;
        public NoCommentsFoundException()
        {
            msg = "No Commenst Found!";
        }
        public override string Message => msg;
    }
}
