namespace BloggingApp.Exceptions.HashTagsExceptions
{
    public class NoHashTagsFoundException:Exception
    {
        string msg;
        public NoHashTagsFoundException()
        {
            msg = "No HashTags Found!";
        }
        public override string Message => msg;
    }
}
